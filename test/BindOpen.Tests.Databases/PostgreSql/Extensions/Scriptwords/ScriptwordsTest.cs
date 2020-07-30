using BindOpen.Application.Scopes;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Databases.Data.Queries;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Scriptwords
{
    [TestFixture, Order(12)]
    public class ScriptwordsTest
    {
        IBdoDbConnector _dbConnector;

        [SetUp]
        public void Setup()
        {
            _dbConnector = GlobalVariables.AppHost.CreatePostgreSqlConnector();
        }

        [Test, Order(1)]
        public void TestInterprete_Fun_SqlEq()
        {
            var log = new BdoLog();

            string value = null;
            string fluentScript = DbFluent.Eq(
                DbFluent.Field("RegionalDirectorateId"), DbFluent.IfNull(value, DbFluent.Field("RegionalDirectorateId")));
            string expectedScript = "$sqlEq($sqlField('RegionalDirectorateId'), $sqlIfNull($sqlNull(), $sqlField('RegionalDirectorateId')))";

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(expectedScript.Equals(fluentScript, StringComparison.OrdinalIgnoreCase), "Bad fluent interpretation. Result was '" + xml);


            var scriptVariableSet = new ScriptVariableSet();
            scriptVariableSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript, DataExpressionKind.Script, scriptVariableSet, log: log)?.ToString();

            string expectedResult = @"""RegionalDirectorateId""=COALESCE(NULL, ""RegionalDirectorateId"")";

            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(2)]
        public void TestInterprete_Fun_SqlEq_Null()
        {
            var log = new BdoLog();

            // Case: value, null

            string value = null;
            string fluentScript1 = DbFluent.Eq(
                null, DbFluent.IfNull(value, DbFluent.Field("RegionalDirectorateId", DbFluent.Table("Table1", "Schema1"))));

            var scriptVariableSet = new ScriptVariableSet();
            scriptVariableSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript1, DataExpressionKind.Script, scriptVariableSet, log: log)?.ToString();

            string expectedResult = @"COALESCE(NULL, ""Schema1"".""Table1"".""RegionalDirectorateId"") is null";

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);

            // Case: null, value

            string fluentScript2 = DbFluent.Eq(
                DbFluent.IfNull(value, DbFluent.Field("RegionalDirectorateId", DbFluent.Table("Table1", "Schema1"))), null);
            result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript2, DataExpressionKind.Script, scriptVariableSet, log: log)?.ToString();

            expectedResult = @"COALESCE(NULL, ""Schema1"".""Table1"".""RegionalDirectorateId"") is null";

            xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(3)]
        public void TestInterprete_Fun_SqlIfNull()
        {
            string value = null;
            string script1 = DbFluent.Eq(
                DbFluent.IfNull(DbFluent.Field("RegionalDirectorateId"), ""),
                DbFluent.IfNull(value, ""));

            string expectedResult = @"COALESCE(""RegionalDirectorateId"", '')=COALESCE(NULL, '')";

            var log = new BdoLog();

            var scriptVariableSet = new ScriptVariableSet();
            scriptVariableSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script1, DataExpressionKind.Script, scriptVariableSet, log: log)?.ToString();

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(3)]
        public void TestInterprete_Fun_SqlEncode()
        {
            string script = DbFluent.EncodeBase64(DbFluent.Text("ABCDE"));
            string expectedResult = @"encode('ABCDE', 'base64')";

            var log = new BdoLog();

            var scriptVariableSet = new ScriptVariableSet();
            scriptVariableSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script, DataExpressionKind.Script, scriptVariableSet, log: log)?.ToString();

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);

            script = DbFluent.DecodeBase64(DbFluent.Field("ABCDE"));
            expectedResult = @"decode(""ABCDE"", 'base64')";
            log = new BdoLog();

            scriptVariableSet = new ScriptVariableSet();
            scriptVariableSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script, DataExpressionKind.Script, scriptVariableSet, log: log)?.ToString();

            xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

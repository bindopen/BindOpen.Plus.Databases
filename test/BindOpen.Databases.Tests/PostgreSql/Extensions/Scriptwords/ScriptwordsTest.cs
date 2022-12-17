using BindOpen.Framework.Runtime.Scopes;
using BindOpen.Framework.MetaData.Expression;
using BindOpen.Framework.MetaData.Helpers.Serialization;
using BindOpen.Databases.Data;
using BindOpen.Framework.Extensions.Connectors;
using BindOpen.Logging;
using BindOpen.System.Scripting;
using NUnit.Framework;
using System;
using BindOpen.Databases.Connecting.Interfaces;

namespace BindOpen.Databases.Tests.PostgreSql.Data.Scriptwords
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


            var varElementSet = new ScriptVariableSet();
            varElementSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript, DataExpressionKind.Script, varElementSet, log: log)?.ToString();

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

            var varElementSet = new ScriptVariableSet();
            varElementSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript1, DataExpressionKind.Script, varElementSet, log: log)?.ToString();

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
            result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript2, DataExpressionKind.Script, varElementSet, log: log)?.ToString();

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

            var varElementSet = new ScriptVariableSet();
            varElementSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script1, DataExpressionKind.Script, varElementSet, log: log)?.ToString();

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

            var varElementSet = new ScriptVariableSet();
            varElementSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script, DataExpressionKind.Script, varElementSet, log: log)?.ToString();

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);

            script = DbFluent.DecodeBase64(DbFluent.Field("ABCDE"));
            expectedResult = @"decode(""ABCDE"", 'base64')";
            log = new BdoLog();

            varElementSet = new ScriptVariableSet();
            varElementSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script, DataExpressionKind.Script, varElementSet, log: log)?.ToString();

            xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(4)]
        public void TestInterprete_Fun_SqlULCase()
        {
            var log = new BdoLog();

            string fluentScript = DbFluent.LowerCase(DbFluent.Field("RegionalDirectorateId"));
            string expectedScript = "$sqlLCase($sqlField('RegionalDirectorateId'))";

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(expectedScript.Equals(fluentScript, StringComparison.OrdinalIgnoreCase), "Bad fluent interpretation. Result was '" + xml);


            var varElementSet = new ScriptVariableSet();
            varElementSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript, DataExpressionKind.Script, varElementSet, log: log)?.ToString();

            string expectedResult = @"lower(""RegionalDirectorateId"")";

            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(5)]
        public void TestInterprete_Fun_SqlULPad()
        {
            var log = new BdoLog();

            string fluentScript = DbFluent.LeftPadding(DbFluent.Field("RegionalDirectorateId"), 10, DbFluent.Text("A"));

            var varElementSet = new ScriptVariableSet();
            varElementSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript, DataExpressionKind.Script, varElementSet, log: log)?.ToString();

            string expectedResult = @"lpad(""RegionalDirectorateId"", 10, 'A')";

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(3)]
        public void TestInterprete_Fun_SqlIf()
        {
            string value = null;
            string script1 = DbFluent.If(DbFluent.IsNull(value), DbFluent.Field("RegionalDirectorateId"), DbFluent.Field("RegionalDirectorateId2"));

            string expectedResult = @"case when (null is null) then ""RegionalDirectorateId"" else ""RegionalDirectorateId2"" end";

            var log = new BdoLog();

            var varElementSet = new ScriptVariableSet();
            varElementSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script1, DataExpressionKind.Script, varElementSet, log: log)?.ToString();

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

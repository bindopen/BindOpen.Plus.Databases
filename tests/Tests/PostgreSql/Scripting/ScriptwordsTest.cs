using BindOpen.Databases.Connectors;
using BindOpen.Plus.Databases.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Databases.PostgreSql.Scripting
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
            var log = BdoLogging.NewLog();

            string value = null;
            string fluentScript = BdoDb.Eq(
                BdoDb.Field("RegionalDirectorateId"), BdoDb.IfNull(value, BdoDb.Field("RegionalDirectorateId")));
            string expectedScript = "$sqlEq($sqlField('RegionalDirectorateId'), $sqlIfNull($sqlNull(), $sqlField('RegionalDirectorateId')))";

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(expectedScript.Equals(fluentScript, StringComparison.OrdinalIgnoreCase), "Bad fluent interpretation. Result was '" + xml);


            var varSet = new ScriptVariableSet();
            varSet.SetValue(VarSetDb.__DbBuilder,
                BdoDb.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript, DataExpressionKind.Script, varSet, log: log)?.ToString();

            string expectedResult = @"""RegionalDirectorateId""=COALESCE(NULL, ""RegionalDirectorateId"")";

            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(2)]
        public void TestInterprete_Fun_SqlEq_Null()
        {
            var log = BdoLogging.NewLog();

            // Case: value, null

            string value = null;
            string fluentScript1 = BdoDb.Eq(
                null, BdoDb.IfNull(value, BdoDb.Field("RegionalDirectorateId", BdoDb.Table("Table1", "Schema1"))));

            var varSet = new ScriptVariableSet();
            varSet.SetValue(VarSetDb.__DbBuilder,
                BdoDb.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript1, DataExpressionKind.Script, varSet, log: log)?.ToString();

            string expectedResult = @"COALESCE(NULL, ""Schema1"".""Table1"".""RegionalDirectorateId"") is null";

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);

            // Case: null, value

            string fluentScript2 = BdoDb.Eq(
                BdoDb.IfNull(value, BdoDb.Field("RegionalDirectorateId", BdoDb.Table("Table1", "Schema1"))), null);
            result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript2, DataExpressionKind.Script, varSet, log: log)?.ToString();

            expectedResult = @"COALESCE(NULL, ""Schema1"".""Table1"".""RegionalDirectorateId"") is null";

            xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(3)]
        public void TestInterprete_Fun_SqlIfNull()
        {
            string value = null;
            string script1 = BdoDb.Eq(
                BdoDb.IfNull(BdoDb.Field("RegionalDirectorateId"), ""),
                BdoDb.IfNull(value, ""));

            string expectedResult = @"COALESCE(""RegionalDirectorateId"", '')=COALESCE(NULL, '')";

            var log = BdoLogging.NewLog();

            var varSet = new ScriptVariableSet();
            varSet.SetValue(VarSetDb.__DbBuilder,
                BdoDb.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script1, DataExpressionKind.Script, varSet, log: log)?.ToString();

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(3)]
        public void TestInterprete_Fun_SqlEncode()
        {
            string script = BdoDb.EncodeBase64(BdoDb.Text("ABCDE"));
            string expectedResult = @"encode('ABCDE', 'base64')";

            var log = BdoLogging.NewLog();

            var varSet = new ScriptVariableSet();
            varSet.SetValue(VarSetDb.__DbBuilder,
                BdoDb.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script, DataExpressionKind.Script, varSet, log: log)?.ToString();

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);

            script = BdoDb.DecodeBase64(BdoDb.Field("ABCDE"));
            expectedResult = @"decode(""ABCDE"", 'base64')";
            log = new BdoLog();

            varSet = new ScriptVariableSet();
            varSet.SetValue(VarSetDb.__DbBuilder,
                BdoDb.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script, DataExpressionKind.Script, varSet, log: log)?.ToString();

            xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(4)]
        public void TestInterprete_Fun_SqlULCase()
        {
            var log = BdoLogging.NewLog();

            string fluentScript = BdoDb.LowerCase(BdoDb.Field("RegionalDirectorateId"));
            string expectedScript = "$sqlLCase($sqlField('RegionalDirectorateId'))";

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(expectedScript.Equals(fluentScript, StringComparison.OrdinalIgnoreCase), "Bad fluent interpretation. Result was '" + xml);


            var varSet = new ScriptVariableSet();
            varSet.SetValue(VarSetDb.__DbBuilder,
                BdoDb.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript, DataExpressionKind.Script, varSet, log: log)?.ToString();

            string expectedResult = @"lower(""RegionalDirectorateId"")";

            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(5)]
        public void TestInterprete_Fun_SqlULPad()
        {
            var log = BdoLogging.NewLog();

            string fluentScript = BdoDb.LeftPadding(BdoDb.Field("RegionalDirectorateId"), 10, BdoDb.Text("A"));

            var varSet = new ScriptVariableSet();
            varSet.SetValue(VarSetDb.__DbBuilder,
                BdoDb.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(fluentScript, DataExpressionKind.Script, varSet, log: log)?.ToString();

            string expectedResult = @"lpad(""RegionalDirectorateId"", 10, 'A')";

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(3)]
        public void TestInterprete_Fun_SqlIf()
        {
            string value = null;
            string script1 = BdoDb.If(BdoDb.IsNull(value), BdoDb.Field("RegionalDirectorateId"), BdoDb.Field("RegionalDirectorateId2"));

            string expectedResult = @"case when (null is null) then ""RegionalDirectorateId"" else ""RegionalDirectorateId2"" end";

            var log = BdoLogging.NewLog();

            var varSet = new ScriptVariableSet();
            varSet.SetValue(VarSetDb.__DbBuilder,
                BdoDb.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Evaluate(script1, DataExpressionKind.Script, varSet, log: log)?.ToString();

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

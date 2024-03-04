using BindOpen.Data;
using BindOpen.Databases.Relational.Builders;
using BindOpen.Databases.Connectors;
using BindOpen.Logging;
using BindOpen.Plus.Databases.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Databases.PostgreSql.Scripting
{
    [TestFixture, Order(12)]
    public class ScriptwordsTest
    {
        IBdoDbRelationalConnector _dbConnector;

        [SetUp]
        public void Setup()
        {
            _dbConnector = GlobalVariables.Scope.CreatePostgreSqlConnector();
        }

        [Test, Order(1)]
        public void TestInterprete_Fun_SqlEq()
        {
            var log = BdoLogging.NewLog();

            string value = null;
            var fluentScript = BdoDb.Eq(
                BdoDb.Field("RegionalDirectorateId"), BdoDb.IfNull(value, BdoDb.Field("RegionalDirectorateId")));
            string expectedScript = "$sqlEq($sqlField('RegionalDirectorateId'), $sqlIfNull($sqlNull(), $sqlField('RegionalDirectorateId')))";

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(expectedScript.Equals(fluentScript.ToString(), StringComparison.OrdinalIgnoreCase), "Bad fluent interpretation. Result was '" + xml);


            var varSet = BdoData.NewSet()
                .AddDbBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.Scope);
            string result = GlobalVariables.Scope.Interpreter.Evaluate(fluentScript, varSet, log: log)?.ToString();

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
            var fluentScript1 = BdoDb.Eq(
                null, BdoDb.IfNull(value, BdoDb.Field("RegionalDirectorateId", BdoDb.Table("Table1", "Schema1"))));

            var varSet = BdoData.NewSet()
                .AddDbBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.Scope);
            string result = GlobalVariables.Scope.Interpreter.Evaluate(fluentScript1, varSet, log: log)?.ToString();

            string expectedResult = @"COALESCE(NULL, ""Schema1"".""Table1"".""RegionalDirectorateId"") is null";

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);

            // Case: null, value

            var fluentScript2 = BdoDb.Eq(
                BdoDb.IfNull(value, BdoDb.Field("RegionalDirectorateId", BdoDb.Table("Table1", "Schema1"))), null);
            result = GlobalVariables.Scope.Interpreter.Evaluate(fluentScript2, varSet, log: log)?.ToString();

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
            var script1 = BdoDb.Eq(
                BdoDb.IfNull(BdoDb.Field("RegionalDirectorateId"), ""),
                BdoDb.IfNull(value, ""));

            string expectedResult = @"COALESCE(""RegionalDirectorateId"", '')=COALESCE(NULL, '')";

            var log = BdoLogging.NewLog();

            var varSet = BdoData.NewSet()
                .AddDbBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.Scope);
            string result = GlobalVariables.Scope.Interpreter.Evaluate(script1, varSet, log: log)?.ToString();

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
            var script = BdoDb.EncodeBase64(BdoDb.Text("ABCDE"));
            string expectedResult = @"encode('ABCDE', 'base64')";

            var log = BdoLogging.NewLog();

            var varSet = BdoData.NewSet()
                .AddDbBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.Scope);
            string result = GlobalVariables.Scope.Interpreter.Evaluate(script, varSet, log: log)?.ToString();

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);

            script = BdoDb.DecodeBase64(BdoDb.Field("ABCDE"));
            expectedResult = @"decode(""ABCDE"", 'base64')";
            log = new BdoLog();

            varSet = BdoData.NewSet()
                .AddDbBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.Scope);
            result = GlobalVariables.Scope.Interpreter.Evaluate(script, varSet, log: log)?.ToString();

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

            var fluentScript = BdoDb.LowerCase(BdoDb.Field("RegionalDirectorateId"));
            string expectedScript = "$sqlLCase($sqlField('RegionalDirectorateId'))";

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(expectedScript.Equals(fluentScript.ToString(), StringComparison.OrdinalIgnoreCase), "Bad fluent interpretation. Result was '" + xml);

            var varSet = BdoData.NewSet()
                .AddDbBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.Scope);
            string result = GlobalVariables.Scope.Interpreter.Evaluate(fluentScript, varSet, log: log)?.ToString();

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

            var fluentScript = BdoDb.LeftPadding(BdoDb.Field("RegionalDirectorateId"), 10, BdoDb.Text("A"));

            var varSet = BdoData.NewSet()
                .AddDbBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.Scope);
            string result = GlobalVariables.Scope.Interpreter.Evaluate(fluentScript, varSet, log: log)?.ToString();

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
            var script1 = BdoDb.If(BdoDb.IsNull(value), BdoDb.Field("RegionalDirectorateId"), BdoDb.Field("RegionalDirectorateId2"));

            string expectedResult = @"case when (null is null) then ""RegionalDirectorateId"" else ""RegionalDirectorateId2"" end";

            var log = BdoLogging.NewLog();

            var varSet = BdoData.NewSet()
                .AddDbBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.Scope);
            string result = GlobalVariables.Scope.Interpreter.Evaluate(script1, varSet, log: log)?.ToString();

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

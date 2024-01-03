using BindOpen.Plus.Databases.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Databases.PostgreSql.Queries
{
    [TestFixture]
    public class QueriesBdoDb
    {
        private IBdoHost _appHost;
        private DateTime _value_datetime = new DateTime(2020, 12, 20);

        [SetUp]
        public void Setup()
        {
            _appHost = GlobalVariables.AppHost;
        }

        [Test]
        public void TestSqlValue()
        {
            var expression = BdoDb.Value(_value_datetime);
            var log = BdoLogging.NewLog();

            string expectedResult = @"$sqlValue('2020-12-20T00:00:00')";

            string result = (string)expression;

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void TestSqlLike()
        {
            var expression = BdoDb.Like(
                BdoDb.Table("MyTable"),
                BdoDb.Concat("%", BdoDb.Parameter("myText").AsExp(), "%"));
            var log = BdoLogging.NewLog();

            string expectedResult = @"$sqlLike($sqlTable('MyTable'), $sqlConcat($sqlText('%'), $sqlParameter('myText'), $sqlText('%')))";

            string result = (string)expression;

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);

            // scripted

            result = _appHost.Interpreter.Evaluate<string>(
                expression,
                new ScriptVariableSet().SetDbBuilder(new DbQueryBuilder_PostgreSql()),
                log: log);
            expectedResult = @"(""MyTable"" like concat('%', |*|p:myText|*|, '%'))";
            xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);

        }

        [Test]
        public void TestSqlIfNull()
        {
            var expression = BdoDb.Eq(
                    BdoDb.Field("fieldA"),
                    BdoDb.IfNull(
                        BdoDb.Parameter("myText"),
                        BdoDb.Field("fieldA")));
            var log = BdoLogging.NewLog();

            string expectedResult = @"""fieldA""=coalesce(|*|p:myText|*|, ""fieldA"")";

            var result = _appHost.Interpreter.Evaluate<string>(
                expression,
                new ScriptVariableSet().SetDbBuilder(new DbQueryBuilder_PostgreSql()),
                log: log);

            var xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void TestSqlStringConcat()
        {
            var expression = BdoDb.StringConcat("X", "O", "A");
            var log = BdoLogging.NewLog();

            string expectedResult = @"'X' || 'O' || 'A'";

            var result = _appHost.Interpreter.Evaluate<string>(
                expression,
                new ScriptVariableSet().SetDbBuilder(new DbQueryBuilder_PostgreSql()),
                log: log);

            var xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

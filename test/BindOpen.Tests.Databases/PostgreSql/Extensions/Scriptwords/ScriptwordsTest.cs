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
        public void TestInterprete_Fun_SqlEqual()
        {
            string script = "$ISEQUAL(\"MYTABLE\", $Text(MYTABLE))";
            string expectedResult = "true";

            var log = new BdoLog();
            string result = GlobalVariables.AppHost.Scope.Interpreter.Interprete(script, DataExpressionKind.Script, log: log);
            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(expectedResult.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test, Order(1)]
        public void TestInterprete_Fun_SqlIfNull()
        {
            string value = null;
            string script1 = "$sqlEq($sqlIfNull($sqlField('RegionalDirectorateId'), ''), $sqlIfNull(" + DbFluent.Value(value) + ", ''))";
            string script2 = DbFluent.Eq(
                DbFluent.IfNull(DbFluent.Field("RegionalDirectorateId"), ""),
                    DbFluent.IfNull(value, ""));

            string expectedResult = "true";

            var log = new BdoLog();

            var scriptVariableSet = new ScriptVariableSet();
            scriptVariableSet.SetValue(VarSetDb.__DbBuilder,
                DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(GlobalVariables.AppHost));
            string result = GlobalVariables.AppHost.Scope.Interpreter.Interprete(script1, DataExpressionKind.Script, scriptVariableSet, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(expectedResult.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}

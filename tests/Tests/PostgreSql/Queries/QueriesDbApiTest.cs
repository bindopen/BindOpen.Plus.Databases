using BindOpen.Databases.Connectors;
using BindOpen.Databases.Stores;
using BindOpen.Databases.Tests.Fakes;
using BindOpen.Logging;
using BindOpen.Plus.Databases.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Databases.PostgreSql.Queries
{
    [TestFixture]
    public class QueriesDbApiTest
    {
        DbModelFake _model;
        IBdoDbConnector _dbConnector;

        [SetUp]
        public void Setup()
        {
            _model = GlobalVariables.Scope.GetModel<DbModelFake>();
            _dbConnector = GlobalVariables.Scope.CreatePostgreSqlConnector();
        }

        [Test]
        public void SimpleSelectWithApi1()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") where ""Code""='codeC' limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode1("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelectWithApi2()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") where (""field1""=""field2"") and ""Code""='codeC' limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode2("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelectWithApi3()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") where ""Code""='codeC' order by ""employee"".""Code"" asc, ""regionalDirectorate"".""DateTimeField"" desc limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode3("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

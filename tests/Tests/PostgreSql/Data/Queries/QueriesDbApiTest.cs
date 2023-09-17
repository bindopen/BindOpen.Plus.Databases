using BindOpen.Framework.Runtime.Scopes;
using BindOpen.Framework.MetaData.Helpers.Serialization;
using BindOpen.Framework.MetaData.Stores;
using BindOpen.Framework.Extensions.Connectors;
using BindOpen.Logging;
using BindOpen.Databases.Tests.PostgreSql.Data.Models;
using NUnit.Framework;
using System;
using BindOpen.Databases.Connecting.Interfaces;

namespace BindOpen.Databases.Tests.PostgreSql.Data.Queries
{
    [TestFixture]
    public class QueriesDbApiTest
    {
        TestDbModel _model;
        IBdoDbConnector _dbConnector;

        [SetUp]
        public void Setup()
        {
            _model = GlobalVariables.AppHost.GetModel<TestDbModel>();
            _dbConnector = GlobalVariables.AppHost.CreatePostgreSqlConnector();
        }

        [Test]
        public void SimpleSelectWithApi1()
        {
            var log = new BdoLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") where ""Code""='codeC' limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode1("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelectWithApi2()
        {
            var log = new BdoLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") where (""field1""=""field2"") and ""Code""='codeC' limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode2("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelectWithApi3()
        {
            var log = new BdoLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") where ""Code""='codeC' order by ""employee"".""Code"" asc, ""regionalDirectorate"".""DateTimeField"" desc limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode3("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

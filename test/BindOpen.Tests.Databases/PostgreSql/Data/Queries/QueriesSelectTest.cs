using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.PostgreSql.Data.Models;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Queries
{
    [TestFixture]
    public class QueriesSelectTest
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
        public void SimpleSelect1()
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
        public void SimpleSelect2()
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
        public void SimpleSelect3()
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

        [Test]
        public void SimpleSelect4()
        {
            var log = new BdoLog();

            string expectedResult = @"with ""directorate"" as (select * from ""Mdm"".""RegionalDirectorate"" ) select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" as ""directorate"" on (""Mdm"".""Employee"".""EmployeeId""=""directorate"".""RegionalDirectorateId"") where ""Code""='codeC' limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode4("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelect5()
        {
            var log = new BdoLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" where exists((select * from ""Mdm"".""Employee"" where ""Code""='codeC' ))";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode5("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelect6()
        {
            var log = new BdoLog();

            string expectedResult = @"select ""employee"".*, ""contact"".""Code"" as ""contactCode"", ""regionalDirectorate"".""RegionalDirectorateId"", ""regionalDirectorate"".""Code"", now() from ""Mdm"".""Employee""";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode6("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelect7()
        {
            var log = new BdoLog();

            string expectedResult = @"select ""employee"".*, ""contact"".""Code"" as ""contactCode"", ""regionalDirectorate"".""RegionalDirectorateId"", ""regionalDirectorate"".""Code"", now() from ""Mdm"".""Employee""";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode6("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

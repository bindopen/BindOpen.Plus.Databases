using BindOpen.Databases.Connectors;
using BindOpen.Databases.Tests.Fakes;
using BindOpen.Plus.Databases.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Databases.PostgreSql.Queries
{
    [TestFixture]
    public class QueriesSelectTest
    {
        DbModelFake _model;
        IBdoDbConnector _dbConnector;

        [SetUp]
        public void Setup()
        {
            _model = GlobalVariables.AppHost.GetModel<DbModelFake>();
            _dbConnector = GlobalVariables.AppHost.CreatePostgreSqlConnector();
        }

        [Test]
        public void SimpleSelect1()
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
        public void SimpleSelect2()
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
        public void SimpleSelect3()
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

        [Test]
        public void SimpleSelect4()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"with ""directorate"" as (select * from ""Mdm"".""RegionalDirectorate"" ) select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" as ""directorate"" on (""Mdm"".""Employee"".""EmployeeId""=""directorate"".""RegionalDirectorateId"") where ""Code""='codeC' limit 100";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode4("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelect5()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"select ""Mdm"".""Employee"".*, ""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"", ""Mdm"".""RegionalDirectorate"".""Code"" from ""Mdm"".""Employee"" where exists((select * from ""Mdm"".""Employee"" where ""Code""='codeC' ))";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode5("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelect6()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"select ""employee"".*, ""contact"".""Code"" as ""contactCode"", ""regionalDirectorate"".""RegionalDirectorateId"", ""regionalDirectorate"".""Code"", now() from ""Mdm"".""Employee""";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode6("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleSelect7()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"select ""employee"".*, ""contact"".""Code"" as ""contactCode"", ""regionalDirectorate"".""RegionalDirectorateId"", ""regionalDirectorate"".""Code"", now() from ""Mdm"".""Employee""";

            string result = _dbConnector.CreateCommandText(_model.SelectEmployeeWithCode7("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

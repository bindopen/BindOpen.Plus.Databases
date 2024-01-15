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
    public class QueriesDeleteTest
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
        public void SimpleDelete1()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _dbConnector.CreateCommandText(_model.DeleteEmployee1("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleDelete2()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _dbConnector.CreateCommandText(_model.DeleteEmployee2("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleDelete3()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _dbConnector.CreateCommandText(_model.DeleteEmployee3("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleDelete4()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _dbConnector.CreateCommandText(_model.DeleteEmployee4("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleDelete5()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _dbConnector.CreateCommandText(_model.DeleteEmployee5("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleDelete6()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" using ""Mdm"".""RegionalDirectorate"" as ""directorate"" where (""code""='codeC' and (""Mdm"".""Employee"".""EmployeeId""=""directorate"".""RegionalDirectorateId""))";

            string result = _dbConnector.CreateCommandText(_model.DeleteEmployee6("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleDelete7()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" using ""Mdm"".""Contact"" where (""Code""='codeC' and (""Mdm"".""Employee"".""MainContactId""=""Mdm"".""Contact"".""ContactId""))";

            string result = _dbConnector.CreateCommandText(_model.DeleteEmployee7("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleDelete8()
        {
            var log = BdoLogging.NewLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" using ""Mdm"".""RegionalDirectorate"" as ""directorate"", ""Mdm"".""Contact"", ""Mdm"".""Contact"" as ""secondaryCountry"" where (((""Code""='codeC' and (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"")) and (""Mdm"".""Employee"".""MainContactId""=""Mdm"".""Contact"".""ContactId"")) and (""secondary"".""MainContactId""=""Mdm"".""Contact"".""ContactId""))";

            string result = _dbConnector.CreateCommandText(_model.DeleteEmployee8("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

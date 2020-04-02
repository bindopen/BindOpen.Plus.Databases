using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Databases.Data.Repositories;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.PostgreSql.Data.Models;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Repositories
{
    [TestFixture]
    public class RepositoryTest
    {
        TestRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = GlobalVariables.AppHost.CreateDbRepository<TestRepository, TestDbModel>(
                GlobalVariables.AppHost.CreatePostgreSqlConnector());
        }

        [Test]
        public void SimpleDelete1()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _repository.Connector.CreateCommandText(_repository.Model.DeleteEmployee1("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleDelete2()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _repository.Connector.CreateCommandText(_repository.Model.DeleteEmployee2("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleDelete3()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _repository.Connector.CreateCommandText(_repository.Model.DeleteEmployee3("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleDelete4()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _repository.Connector.CreateCommandText(_repository.Model.DeleteEmployee4("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleDelete5()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" where ""Code""='codeC'";

            string result = _repository.Connector.CreateCommandText(_repository.Model.DeleteEmployee5("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleDelete6()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" using ""Mdm"".""RegionalDirectorate"" as ""directorate"" where (""code""='codeC' and (""Mdm"".""Employee"".""EmployeeId""=""directorate"".""RegionalDirectorateId""))";

            string result = _repository.Connector.CreateCommandText(_repository.Model.DeleteEmployee6("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleDelete7()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" using ""Mdm"".""RegionalDirectorate"" as ""directorate"" where (""code""='codeC' and (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId""))";

            string result = _repository.Connector.CreateCommandText(_repository.Model.DeleteEmployee7("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleDelete8()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" using ""Mdm"".""RegionalDirectorate"" as ""directorate"" where (""code""='codeC' and (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId""))";

            string result = _repository.Connector.CreateCommandText(_repository.Model.DeleteEmployee8("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}

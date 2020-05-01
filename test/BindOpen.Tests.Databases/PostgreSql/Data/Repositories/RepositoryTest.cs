using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Stores;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Repositories
{
    [TestFixture]
    public class RepositoryTest
    {
        TestRepository _repository;

        [SetUp]
        public void TestSetup()
        {
            var host = GlobalVariables.AppHost;
            _repository = host.CreateConnected<TestRepository>(
                host.CreatePostgreSqlConnector(host.GetDatasourceDepot().GetConnectionString("db.test")));
        }

        [Test]
        public void TestRepositoryUsingConnection()
        {
            Assert.That(_repository?.Connector?.ConnectionString != StringHelper.__NoneString,
                "Could not retrieve the string connection of connector");

            var log = new BdoLog();
            _repository.UsingConnection(
                (p, l) =>
                {
                }, log);

            Assert.That(log.HasErrorsOrExceptions(), "Using connection method not working");
        }

        [Test]
        public void TestSimpleDelete1()
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
        public void TestSimpleDelete2()
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
        public void TestSimpleDelete3()
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
        public void TestSimpleDelete4()
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
        public void TestSimpleDelete5()
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
        public void TestSimpleDelete6()
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
        public void TestSimpleDelete7()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" using ""Mdm"".""Contact"" where (""Code""='codeC' and (""Mdm"".""Employee"".""MainContactId""=""Mdm"".""Contact"".""ContactId""))";

            string result = _repository.Connector.CreateCommandText(_repository.Model.DeleteEmployee7("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void TestSimpleDelete8()
        {
            var log = new BdoLog();

            string expectedResult = @"delete from ""Mdm"".""Employee"" using ""Mdm"".""RegionalDirectorate"" as ""directorate"", ""Mdm"".""Contact"", ""Mdm"".""Contact"" as ""secondaryCountry"" where (((""Code""='codeC' and (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"")) and (""Mdm"".""Employee"".""MainContactId""=""Mdm"".""Contact"".""ContactId"")) and (""secondary"".""MainContactId""=""Mdm"".""Contact"".""ContactId""))";

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

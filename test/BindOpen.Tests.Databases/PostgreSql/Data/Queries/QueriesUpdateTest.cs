using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.PostgreSql.Data.Dtos.Test1;
using BindOpen.Tests.Databases.PostgreSql.Data.Models;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Queries
{
    [TestFixture]
    public class QueriesUpdateTest
    {
        TestDbModel _model;
        IBdoDbConnector _dbConnector;
        EmployeeDto _employee;

        [SetUp]
        public void Setup()
        {
            _model = GlobalVariables.AppHost.GetModel<TestDbModel>();
            _dbConnector = GlobalVariables.AppHost.CreatePostgreSqlConnector();

            _employee = new EmployeeDto()
            {
                Code = "code1",
                ContactEmail = "email@email.com",
                FisrtName = "firstName",
                LastName = "lastName",
                RegionalDirectorateCode = "FR",
                StaffNumber = null
            };
        }

        [Test]
        public void SimpleUpdate1()
        {
            var log = new BdoLog();

            string expectedResult = @"update ""Mdm"".""Employee"" set ""Code""='codeC',""ContactEmail""='email@email.com',""FisrtName""='firstName',""LastName""='lastName',""RegionalDirectorateId""=(select ""RegionalDirectorateId"" from ""Mdm"".""RegionalDirectorate"" where ""Code""=NULL) from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") returning ""Mdm"".""Employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee1("codeC", true, _employee), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleUpdate2()
        {
            var log = new BdoLog();

            string expectedResult = @"update ""Mdm"".""Employee"" as ""employee"" set ""RegionalDirectorateId"" is null from ""Mdm"".""RegionalDirectorate"" as ""regionalDirectorate"" where ""employee"".""RegionalDirectorateId""=""regionalDirectorate"".""RegionalDirectorateId"" and ""employee"".""Code""='codeC' returning ""employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee2("codeC"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }

        [Test]
        public void SimpleUpdate3()
        {
            var log = new BdoLog();

            string expectedResult = @"update ""Mdm"".""Employee"" as ""employee"" set ""RegionalDirectorateId"" is null from ""Mdm"".""RegionalDirectorate"" as ""regionalDirectorate"" where ""employee"".""RegionalDirectorateId""=""regionalDirectorate"".""RegionalDirectorateId"" and ""employee"".""Code""='codeR' returning ""employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee3("codeR"), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}

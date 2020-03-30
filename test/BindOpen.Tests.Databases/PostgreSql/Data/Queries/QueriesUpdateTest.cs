using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.Data.Dtos.Test1;
using BindOpen.Tests.Databases.Data.Models;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.Data.Queries
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
                StaffNumber = "123"
            };
        }

        [Test]
        public void SimpleUpdate1()
        {
            var log = new BdoLog();

            string expectedResult = @"update ""Mdm"".""Employee"" set ""Code""='codeC',""ContactEmail""='email@email.com',""FisrtName""='firstName',""LastName""='lastName',""StaffNumber""='123',""RegionalDirectorateId""=(select ""RegionalDirectorateId"" from ""Mdm"".""RegionalDirectorate"" where ""Code""=NULL) from ""Mdm"".""Employee"" left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"") returning ""Mdm"".""Employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee1("codeC", true, _employee));

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

            string expectedResult = @"update ""Mdm"".""Employee"" as ""employee"" set ""RegionalDirectorateId""=NULL from ""Mdm"".""RegionalDirectorate"" as ""regionalDirectorate"" where ""employee"".""RegionalDirectorateId""=""regionalDirectorate"".""RegionalDirectorateId"" and ""employee"".""Code""='codeC' returning ""employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee2("codeC"));

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

            string expectedResult = @"update ""Mdm"".""Employee"" as ""employee"" set ""RegionalDirectorateId""=NULL from ""Mdm"".""RegionalDirectorate"" as ""regionalDirectorate"" where ""employee"".""RegionalDirectorateId""=""regionalDirectorate"".""RegionalDirectorateId"" and ""employee"".""Code""='codeR' returning ""employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee3("codeR"));

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}

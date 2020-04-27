using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.PostgreSql.Data.Dtos.Test1;
using BindOpen.Tests.Databases.PostgreSql.Data.Models;
using Bogus;
using Bogus.Extensions;
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

            var f = new Faker();
            _employee = new EmployeeDto()
            {
                Code = f.Lorem.Sentence(),
                ByteArrayField = f.Random.Bytes(1500),
                DateTimeField = f.Date.Soon().OrNull(f),
                DoubleField = f.Random.Double(),
                LongField = f.Random.Long()
            };
        }

        [Test]
        public void SimpleUpdate1()
        {
            var log = new BdoLog();
            string code = "codeC";

            string expectedResult =
                @"update ""Mdm"".""Employee"" set "
                + @"""Code""='" + code + "'"
                + @",""ByteArrayField""=encode('" + _employee.ByteArrayField.ToString(DataValueType.ByteArray) + "', 'base64')"
                + @",""DoubleField""=" + _employee.DoubleField.ToString(DataValueType.Number)
                + @",""DateTimeField""='" + _employee.DateTimeField.ToString(DataValueType.Date) + "'"
                + @",""LongField""=" + _employee.LongField.ToString(DataValueType.Long)
                + @" from ""Mdm"".""Employee"" "
                + @"left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"")"
                + @" returning ""Mdm"".""Employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee1(code, true, _employee), log: log);

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

            string expectedResult =
                @"update ""Mdm"".""Employee"" as ""employee"" set ""RegionalDirectorateId""=null from ""Mdm"".""RegionalDirectorate"" as ""regionalDirectorate"" where ""employee"".""RegionalDirectorateId""=""regionalDirectorate"".""RegionalDirectorateId"" and ""employee"".""Code""='codeC' returning ""employee"".""Code""";

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

            string expectedResult =
                @"update ""Mdm"".""Employee"" as ""employee"" set "
                + @"""RegionalDirectorateId""=null"
                + @",""LongField""=2500"
                + @" from ""Mdm"".""RegionalDirectorate"" as ""regionalDirectorate"""
                + @" where ""employee"".""RegionalDirectorateId""=""regionalDirectorate"".""RegionalDirectorateId"" and ""employee"".""Code""='codeR' returning ""employee"".""Code""";

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

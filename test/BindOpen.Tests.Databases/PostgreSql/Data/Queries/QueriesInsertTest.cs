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
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Queries
{
    [TestFixture]
    public class QueriesInsertTest
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
                DateTimeField = f.Date.Soon(),
                DoubleField = f.Random.Double(),
                LongField = f.Random.Long()
            };
        }

        [Test]
        public void SimpleInsert1()
        {
            var log = new BdoLog();

            string expectedResult =
                @"insert into ""Mdm"".""Employee"" (""Code"", ""ByteArrayField"", ""DoubleField"", ""DateTimeField"", ""LongField"") "
                + "(values ('" + _employee.Code.Replace("'", "''") + "'"
                + ", '" + _employee.ByteArrayField.ToString(DataValueTypes.ByteArray).Replace("'", "''") + "'"
                + ", " + _employee.DoubleField.ToString(DataValueTypes.Number)
                + ", '" + _employee.DateTimeField.ToString(DataValueTypes.Date) + "'"
                + ", " + _employee.LongField.ToString(DataValueTypes.Long) + @"))"
                + @" returning ""Mdm"".""Employee"".""EmployeeId""";

            string result = _dbConnector.CreateCommandText(_model.InsertEmployee1(_employee), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleInsert2()
        {
            var log = new BdoLog();

            string expectedResult =
                @"insert into ""Mdm"".""Employee"" (""Code"", ""ByteArrayField"", ""DoubleField"", ""DateTimeField"", ""LongField"") "
                + @"(select "
                + "'" + _employee.Code.Replace("'", "''") + "'"
                + ", '" + _employee.ByteArrayField.ToString(DataValueTypes.ByteArray).Replace("'", "''") + "'"
                + ", " + _employee.DoubleField.ToString(DataValueTypes.Number)
                + ", '" + _employee.DateTimeField.ToString(DataValueTypes.Date) + "'"
                + ", " + _employee.LongField.ToString(DataValueTypes.Long) + @" from ""Mdm"".""Employee"" where ""Code""='oldCode' )"
                + @" returning ""Mdm"".""Employee"".""EmployeeId""";

            string result = _dbConnector.CreateCommandText(_model.InsertEmployee2(_employee), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleInsert3()
        {
            var log = new BdoLog();

            string expectedResult =
                @"insert into ""Mdm"".""Employee"" (""Code"", ""ByteArrayField"", ""DoubleField"", ""DateTimeField"", ""LongField"") "
                + @"(select "
                + "'" + _employee.Code.Replace("'", "''") + "'"
                + ", '" + _employee.ByteArrayField.ToString(DataValueTypes.ByteArray).Replace("'", "''") + "'"
                + ", " + _employee.DoubleField.ToString(DataValueTypes.Number)
                + ", '" + _employee.DateTimeField.ToString(DataValueTypes.Date) + "'"
                + ", " + _employee.LongField.ToString(DataValueTypes.Long) + @" from ""Mdm"".""Employee"" where ""Code""='oldCode' )"
                + @" returning ""Mdm"".""Employee"".""EmployeeId""";

            string result = _dbConnector.CreateCommandText(_model.InsertEmployee3(_employee), log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

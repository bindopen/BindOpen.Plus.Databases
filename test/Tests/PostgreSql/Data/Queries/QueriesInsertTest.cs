using BindOpen.Framework.Runtime.Scopes;
using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Helpers;
using BindOpen.Framework.MetaData.Helpers.Serialization;
using BindOpen.Framework.MetaData.Stores;
using BindOpen.Framework.Extensions.Connectors;
using BindOpen.Logging;
using BindOpen.Databases.Tests.PostgreSql.Data.Dtos.Test1;
using BindOpen.Databases.Tests.PostgreSql.Data.Models;
using Bogus;
using NUnit.Framework;
using System;
using BindOpen.Databases.Connecting.Interfaces;

namespace BindOpen.Databases.Tests.PostgreSql.Data.Queries
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
        public void SimpleInsertWithValues()
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
        public void SimpleInsertWithSelect()
        {
            var log = new BdoLog();

            string expectedResult =
                @"insert into ""Mdm"".""Employee"" (""Code"", ""ByteArrayField"", ""DoubleField"", ""DateTimeField"", ""LongField"") "
                + @"(select "
                + "'" + _employee.Code.Replace("'", "''") + "'"
                + ", '" + _employee.ByteArrayField.ToString(DataValueTypes.ByteArray).Replace("'", "''") + "'"
                + ", " + _employee.DoubleField.ToString(DataValueTypes.Number)
                + ", '" + _employee.DateTimeField.ToString(DataValueTypes.Date) + "'"
                + ", " + _employee.LongField.ToString(DataValueTypes.Long) + @" where ""Code""='oldCode' )"
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
        public void SimpleInsertWithSelectWithSubQuery()
        {
            var log = new BdoLog();

            string expectedResult =
                @"insert into ""Mdm"".""Employee"" (""Code"", ""ByteArrayField"", ""DoubleField"", ""DateTimeField"", ""LongField"") "
                + @"(select "
                + "'" + _employee.Code.Replace("'", "''") + "'"
                + ", '" + _employee.ByteArrayField.ToString(DataValueTypes.ByteArray).Replace("'", "''") + "'"
                + ", " + _employee.DoubleField.ToString(DataValueTypes.Number)
                + ", '" + _employee.DateTimeField.ToString(DataValueTypes.Date) + "'"
                + ", " + _employee.LongField.ToString(DataValueTypes.Long)
                + @", (select ""Mdm"".""Contact"".""ContactId"" from ""Mdm"".""Contact"" where ""Code""='contactCodeA' limit 1)"
                + @"  from ""Mdm"".""Employee"" where ""Code""='oldCode' )"
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

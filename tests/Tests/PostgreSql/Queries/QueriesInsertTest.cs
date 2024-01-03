using BindOpen.Databases.Connectors;
using BindOpen.Databases.Tests.Fakes;
using BindOpen.Plus.Databases.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Databases.PostgreSql.Queries
{
    [TestFixture]
    public class QueriesInsertTest
    {
        DbModelFake _model;
        IBdoDbConnector _dbConnector;
        EmployeeDtoFake _employee;

        [SetUp]
        public void Setup()
        {
            _model = GlobalVariables.AppHost.GetModel<DbModelFake>();
            _dbConnector = GlobalVariables.AppHost.CreatePostgreSqlConnector();

            var f = new Faker();
            _employee = new EmployeeDtoFake()
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

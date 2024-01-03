using BindOpen.Databases.Connectors;
using BindOpen.Databases.Tests.Fakes;
using BindOpen.Plus.Databases.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Databases.PostgreSql.Queries
{
    [TestFixture]
    public class QueriesUpdateTest
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
        public void SimpleUpdate1()
        {
            var log = BdoLogging.NewLog();
            string code = "codeC";

            string expectedResult =
                @"update ""Mdm"".""Employee"" set "
                + @"""Code""='" + code + "'"
                + @", ""ByteArrayField""='" + _employee.ByteArrayField.ToString(DataValueTypes.ByteArray).Replace("'", "''") + "'"
                + @", ""DoubleField""=" + _employee.DoubleField.ToString(DataValueTypes.Number)
                + @", ""DateTimeField""='" + _employee.DateTimeField.ToString(DataValueTypes.Date) + "'"
                + @", ""LongField""=" + _employee.LongField.ToString(DataValueTypes.Long)
                + @" from ""Mdm"".""Employee"" "
                + @"left join ""Mdm"".""RegionalDirectorate"" on (""Mdm"".""Employee"".""EmployeeId""=""Mdm"".""RegionalDirectorate"".""RegionalDirectorateId"")"
                + @" returning ""Mdm"".""Employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee1(code, true, _employee), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleUpdate2()
        {
            var log = BdoLogging.NewLog();

            string expectedResult =
                @"update ""Mdm"".""Employee"" as ""employee"" set ""RegionalDirectorateId""=null from ""Mdm"".""RegionalDirectorate"" as ""regionalDirectorate"" where ""employee"".""RegionalDirectorateId""=""regionalDirectorate"".""RegionalDirectorateId"" and ""employee"".""Code""='codeC' returning ""employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee2("codeC"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleUpdate3()
        {
            var log = BdoLogging.NewLog();

            string expectedResult =
                @"update ""Mdm"".""Employee"" as ""employee"" set "
                + @"""RegionalDirectorateId""=null"
                + @", ""LongField""=2500"
                + @" from ""Mdm"".""RegionalDirectorate"" as ""regionalDirectorate"""
                + @" where ""employee"".""RegionalDirectorateId""=""regionalDirectorate"".""RegionalDirectorateId"" and ""employee"".""Code""='codeR' returning ""employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee3("codeR"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test]
        public void SimpleUpdate4()
        {
            var log = BdoLogging.NewLog();

            string expectedResult =
                @"update ""Mdm"".""Employee"" as ""employee"" set "
                + @"""RegionalDirectorateId""=null"
                + @", ""LongField""=2500"
                + @" where ""employee"".""RegionalDirectorateId""=""regionalDirectorate"".""RegionalDirectorateId"" and ""employee"".""Code""='codeR' returning ""employee"".""Code""";

            string result = _dbConnector.CreateCommandText(_model.UpdateEmployee4("codeR"), log: log);

            string xml = "";
            if (log.HasErrorOrException())
            {
                xml = ". Result was '" + log.ToString();
            }
            Assert.That(result.Trim().Equals(expectedResult.Trim(), StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}

using BindOpen.Databases.Connectors;
using BindOpen.Databases.Models;
using BindOpen.Databases.Stores;
using BindOpen.Databases.Tests.Fakes;
using BindOpen.Plus.Databases.Tests;
using NUnit.Framework;

namespace BindOpen.Databases.PostgreSql.Tuples
{
    [TestFixture]
    public class TuplesTest
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
        public void AddFields()
        {
            var tuple = BdoDb.Tuple(
                BdoDb.Field("field1", BdoDb.Table("Table1")),
                BdoDb.Field("field2", BdoDb.Table("Table1")),
                BdoDb.Field("field3", BdoDb.Table("Table1")).WithAlias("f3"))
                .AddFields(
                    BdoDb.FieldAsLiteral("field1", 1),
                    BdoDb.FieldAsLiteral("field2", BdoDb.Table("Table2"), 2).WithAlias("f3"));

            Assert.That(tuple.Fields.Count == 3, "Bad script interpretation");
        }
    }
}

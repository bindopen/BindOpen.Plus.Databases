using BindOpen.Application.Scopes;
using BindOpen.Data.Stores;
using BindOpen.Databases.Data.Queries;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.PostgreSql.Data.Models;
using NUnit.Framework;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Tuples
{
    [TestFixture]
    public class TuplesTest
    {
        TestDbModel _model;
        IBdoDbConnector _dbConnector;

        [SetUp]
        public void Setup()
        {
            _model = GlobalVariables.AppHost.GetModel<TestDbModel>();
            _dbConnector = GlobalVariables.AppHost.CreatePostgreSqlConnector();
        }

        [Test]
        public void AddFields()
        {
            var log = new BdoLog();

            var tuple = DbFluent.Tuple(
                DbFluent.Field("field1", DbFluent.Table("Table1")),
                DbFluent.Field("field2", DbFluent.Table("Table1")),
                DbFluent.Field("field3", DbFluent.Table("Table1")).WithAlias("f3"))
                .AddFields(
                    DbFluent.FieldAsLiteral("field1", 1),
                    DbFluent.FieldAsLiteral("field2", DbFluent.Table("Table2"), 2).WithAlias("f3"));

            Assert.That(tuple.Fields.Count == 3, "Bad script interpretation");
        }
    }
}

using BindOpen.Framework.Runtime.Scopes;
using BindOpen.Framework.MetaData.Stores;
using BindOpen.Databases.Data;
using BindOpen.Framework.Extensions.Connectors;
using BindOpen.Logging;
using BindOpen.Databases.Tests.PostgreSql.Data.Models;
using NUnit.Framework;
using BindOpen.Databases.Connecting.Interfaces;

namespace BindOpen.Databases.Tests.PostgreSql.Data.Tuples
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

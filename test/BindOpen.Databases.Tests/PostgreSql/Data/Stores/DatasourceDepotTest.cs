using BindOpen.Framework.MetaData.Helpers.Strings;
using BindOpen.Framework.MetaData.Stores;
using NUnit.Framework;

namespace BindOpen.Databases.Tests.PostgreSql.Data.Stores
{
    [TestFixture]
    public class DatasourceDepotTest
    {
        [SetUp]
        public void TestSetup()
        {
            var host = GlobalVariables.AppHost;
        }

        [Test]
        public void TestAddDatasource()
        {
            var depot = GlobalVariables.AppHost.GetDatasourceDepot();
            var connectionStringA = GlobalVariables.AppHost.GetDatasourceDepot().GetConnectionString("db.testA");

            Assert.That(connectionStringA != null && connectionStringA != StringHelper.__NoneString,
                "Could not retrieve the string connection of connector");
        }
    }
}

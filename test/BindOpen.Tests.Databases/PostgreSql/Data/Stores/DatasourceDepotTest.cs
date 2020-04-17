using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Stores;
using NUnit.Framework;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Stores
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

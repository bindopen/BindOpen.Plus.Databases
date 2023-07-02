using BindOpen.Framework.Runtime.Scopes;
using BindOpen.Framework.MetaData.Stores;
using BindOpen.Logging;

namespace Samples.SampleA.Services
{
    public static class Service_Command
    {
        public static void Process(IBdoHost host, IBdoLog log)
        {
            var config = host.DataStore.GetDatasourceDepot()?.GetConnectorConfiguration("db.test", "database.mssqlserver$client");

            //var repo = new TestDbRepository(host?.GetModel<MyDbModel>(), host.CreatePostgreSqlConnector(""));
            //repo.Test();
        }
    }
}
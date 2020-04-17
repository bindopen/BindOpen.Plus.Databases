using BindOpen.Application.Scopes;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a data source extension.
    /// </summary>
    public static class DatasourcesExtension
    {
        /// <summary>
        /// Creates the specified PostgreSql data source.
        /// </summary>
        /// <param name="datasourceDepot"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static Datasource CreateMSSqlServerSqlDatasource(
            this IBdoDatasourceDepot datasourceDepot,
            string name,
            string connectionString,
            IBdoLog log = null)
        {
            var datasource = new Datasource(name, DatasourceKind.Database,
                (datasourceDepot.Scope.CreateConnectorConfiguration("databases.msSqlServer$client", log)
                    as BdoConnectorConfiguration)?.WithConnectionString(connectionString));

            return datasource;
        }
    }
}

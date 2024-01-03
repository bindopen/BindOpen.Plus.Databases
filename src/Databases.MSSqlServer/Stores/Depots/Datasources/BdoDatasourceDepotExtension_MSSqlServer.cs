using BindOpen.Framework.Runtime.Scopes;
using BindOpen.Framework.MetaData.Items;
using BindOpen.Framework.Runtime;
using BindOpen.Logging;

namespace BindOpen.Framework.MetaData.Stores
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
            var datasource = BdoItems.CreateDatasource(name, DatasourceKind.Database)
                .WithConfiguration(
                    (datasourceDepot?.Scope?.CreateConnectorConfiguration("databases.msSqlServer$client", log)
                        as BdoConnectorConfiguration)?.WithConnectionString(connectionString));

            return datasource as Datasource;
        }
    }
}

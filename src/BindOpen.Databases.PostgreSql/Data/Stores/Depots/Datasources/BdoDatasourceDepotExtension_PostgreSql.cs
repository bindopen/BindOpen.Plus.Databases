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
        public static Datasource CreatePostgreSqlDatasource(
            this IBdoDatasourceDepot datasourceDepot,
            string name,
            string connectionString,
            IBdoLog log = null)
        {
            var datasource = ItemFactory.CreateDatasource(name, DatasourceKind.Database)
                .WithConfiguration(
                    (datasourceDepot?.Scope?.CreateConnectorConfiguration("databases.postgresql$client", log)
                        as BdoConnectorConfiguration)?.WithConnectionString(connectionString));

            return datasource as Datasource;
        }
    }
}

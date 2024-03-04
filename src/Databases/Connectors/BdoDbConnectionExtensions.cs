using BindOpen.Data.Stores;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Connectors;
using System.Data;

namespace BindOpen.Databases.Connectors
{
    /// <summary>
    /// This class proposes extensions for database connection.
    /// </summary>
    public static class BdoDbConnectionExtensions
    {
        // Open -------------------------------------

        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="depot">The data source depot to consider.</param>
        /// <param name="dataSourceName">The data source name to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public static T Open<T>(
            this IBdoScope scope,
            IBdoDatasourceDepot depot,
            string dataSourceName,
            string connectorDefinitionUniqueId,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            depot ??= scope?.DepotStore?.Get<IBdoDatasourceDepot>();

            if (depot == null)
                log?.AddEvent(EventKinds.Error, "Data source depot missing");
            else if (!depot.Has(dataSourceName))
                log?.AddEvent(EventKinds.Error, "Data source '" + dataSourceName + "' missing in depot");
            else
                return scope.Open<T>(depot, dataSourceName, connectorDefinitionUniqueId, log);

            return default;
        }

        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="dataSourceName">The data source name to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public static T Open<T>(
            this IBdoScope scope,
            string dataSourceName,
            string connectorDefinitionUniqueId,
            IBdoLog log = null)
            where T : class, IBdoConnection
        {
            return scope.Open<T>(null, dataSourceName, connectorDefinitionUniqueId, log);
        }

        // Transactions ----------------------------------------------

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <returns></returns>
        public static IDbTransaction BeginTransaction(this IBdoDbConnection connection)
            => connection?.Native?.BeginTransaction();

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns></returns>
        public static IDbTransaction BeginTransaction(this IBdoDbConnection connection, IsolationLevel isolationLevel)
            => connection?.Native?.BeginTransaction(isolationLevel);

        /// <summary>
        /// Gets the identity of the last inserted item
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="id">The long identifier to populate.</param>
        /// <param name="log">The log to consider.</param>
        public static void GetIdentity(
            this IBdoDbConnection connection,
            ref long id,
            IBdoLog log = null)
        {
            connection?.GetIdentity(ref id, log);
        }
    }
}

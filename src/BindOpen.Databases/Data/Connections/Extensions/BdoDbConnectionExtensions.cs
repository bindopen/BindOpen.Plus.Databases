using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Databases.Data.Queries;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Data;

namespace BindOpen.Data.Connections
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
            if (log == null) log = new BdoLog();

            if (depot == null)
                depot = scope?.DataStore?.Get<IBdoDatasourceDepot>();

            if (depot == null)
                log.AddError("Data source depot missing");
            else if (!depot.HasItem(dataSourceName))
                log.AddError("Data source '" + dataSourceName + "' missing in depot");
            else
                return scope.Open<T>(depot.GetItem(dataSourceName), connectorDefinitionUniqueId, log);

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

        // Commands ----------------------------------------------

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="commandText">The command text to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public static IDbCommand CreateCommand(
            this IBdoDbConnection connection,
            string commandText)
        {
            var command = connection?.Native?.CreateCommand();
            command.CommandText = commandText;
            return command;
        }

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public static IDbCommand CreateCommand(
            this IBdoDbConnection connection,
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            IDbCommand command = (connection?.Connector as BdoDbConnector)?.CreateCommand(query, parameterMode, parameterSet, scriptVariableSet, log);
            command.Connection = connection?.Native;

            return command;
        }

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public static IDbCommand CreateCommand<T>(
            this IDbQuery query,
            DbQueryParameterMode parameterMode,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null) where T : BdoDbConnector, new()
        {
            T connector = new T();
            return (connector?.CreateConnection() as IBdoDbConnection)?.CreateCommand(query, parameterMode, parameterSet, scriptVariableSet, log);
        }

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="connection">The connection to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public static IDbCommand CreateCommand<T>(
            this IDbConnection connection,
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null) where T : BdoDbConnector, new()
        {
            T connector = new T();
            var command = connection.CreateCommand();
            command.CommandText = connector?.CreateCommandText(query, parameterMode, parameterSet, scriptVariableSet, log);
            return command;
        }

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="transaction">The transaction to consider.</param>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public static IDbCommand CreateCommand<T>(
            this IDbTransaction transaction,
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null) where T : BdoDbConnector, new()
        {
            IDbCommand command = transaction?.Connection?.CreateCommand<T>(query, parameterMode, parameterSet, scriptVariableSet, log);
            command.Transaction = transaction;

            return command;
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

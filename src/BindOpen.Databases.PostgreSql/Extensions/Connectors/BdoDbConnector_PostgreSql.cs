using BindOpen.Databases.Data;
using BindOpen.Framework.MetaData.Elements;
using BindOpen.Framework.Runtime.Scopes;
using BindOpen.Logging;
using Npgsql;
using System;
using System.Data;

namespace BindOpen.Framework.Extensions.Connectors
{
    /// <summary>
    /// This class represents a OleDb database connector.
    /// </summary>
    [BdoConnector(Name = "databases.postgresql$client")]
    public class BdoDbConnector_PostgreSql : BdoDbConnector
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnector_PostgreSql class.
        /// </summary>
        public BdoDbConnector_PostgreSql() : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnector_PostgreSql class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        public BdoDbConnector_PostgreSql(
            string name, string connectionString = null)
            : base(BdoDbConnectorKind.PostgreSql, name, connectionString)
        {
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        /// <summary>
        /// Updates this instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns the database builder.</returns>
        public override IBdoConnector WithScope(IBdoScope scope)
        {
            _queryBuilder = DbQueryFactory.CreateQueryBuilder<DbQueryBuilder_PostgreSql>(scope);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public override IBdoConnection CreateConnection(IBdoLog log = null)
        {
            IBdoDbConnection connection = null;

            if (!Check<BdoDbConnector_PostgreSql>().AddEventsTo(log, p => p.HasErrorsOrExceptions()).HasErrorsOrExceptions())
            {
                try
                {
                    var dbConnection = new NpgsqlConnection(ConnectionString);
                    if (dbConnection != null)
                    {
                        connection = new BdoDbConnection(this, dbConnection);
                    }
                }
                catch (Exception ex)
                {
                    log.AddException("An exception occured while trying to create connection",
                        description: ex.ToString());
                }
            }

            return connection;
        }

        /// <summary>
        /// Creates a command from the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the database command.</returns>
        public override IDbCommand CreateCommand(
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IDataElementSet parameterSet = null,
            IDataElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var command = new NpgsqlCommand(CreateCommandText(query, parameterMode, parameterSet, varElementSet, log));

            if (query.ParameterSet != null)
            {
                foreach (var parameter in query.ParameterSet.Items)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.GetValue());
                }
            }

            return command;
        }

        #endregion
    }
}

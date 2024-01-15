using BindOpen.Data.Meta;
using BindOpen.Databases.Builders;
using BindOpen.Databases.Models;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Connectors;
using Npgsql;
using System.Data;

namespace BindOpen.Databases.Connectors
{
    /// <summary>
    /// This class represents a OleDb database connector.
    /// </summary>
    [BdoConnector(Name = "databases.postgresql$client")]
    public class BdoDbConnector_PostgreSql : BdoDbConnector
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private NpgsqlConnection _connection;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnector_PostgreSql class.
        /// </summary>
        public BdoDbConnector_PostgreSql() : base()
        {
            _queryBuilder = new DbQueryBuilder_PostgreSql();
        }

        #endregion

        #region BdoDbConnector

        public override IBdoConnection NewConnection(IBdoLog log = null)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creates a command from the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="varSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the database command.</returns>
        public override IDbCommand CreateCommand(
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var command = new NpgsqlCommand(CreateCommandText(query, parameterMode, parameterSet, varSet, log));

            if (query.ParameterSet != null)
            {
                foreach (var parameter in query.ParameterSet.Items)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.GetData());
                }
            }

            return command;
        }

        #endregion
    }
}

using BindOpen.Data.Meta;
using BindOpen.Databases.Relational;
using BindOpen.Databases.Relational.Builders;
using BindOpen.Logging;
using System.Data;

namespace BindOpen.Databases.Connectors
{
    /// <summary>
    /// This class defines a database connector.
    /// </summary>
    public abstract class BdoDbRelationalConnector : BdoDbConnector, IBdoDbRelationalConnector
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector class.
        /// </summary>
        protected BdoDbRelationalConnector() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector class.
        /// </summary>
        /// <param name="kind">The database kind of this instance.</param>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        protected BdoDbRelationalConnector(BdoDbConnectorKind kind) : base(kind)
        {
        }

        #endregion

        // -----------------------------------------------
        // IBdoDbRelationalConnector Implementation
        // -----------------------------------------------

        #region IBdoDbRelationalConnector

        /// <summary>
        /// The query builder of this instance.
        /// </summary>
        protected IDbQueryBuilder _queryBuilder;

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public virtual string CreateCommandText(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (_queryBuilder == null)
            {
                log?.AddEvent(EventKinds.Error, "Data builder missing");
                return null;
            }

            string sqlText = _queryBuilder.BuildQuery(query, parameterMode, parameterSet, varSet, log);
            return sqlText;
        }

        public abstract IDbCommand CreateCommand(
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        #endregion
    }
}

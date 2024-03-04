using BindOpen.Data.Meta;
using BindOpen.Databases.Relational;
using BindOpen.Logging;
using System.Data;

namespace BindOpen.Databases.Connectors
{
    /// <summary>
    /// This class defines a database connector.
    /// </summary>
    public interface IBdoDbRelationalConnector : IBdoDbConnector
    {
        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        string CreateCommandText(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        IDbCommand CreateCommand(
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}

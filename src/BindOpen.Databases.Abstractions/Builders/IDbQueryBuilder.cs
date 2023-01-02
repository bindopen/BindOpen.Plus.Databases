using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Databases.Data;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Databases.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial interface IDbQueryBuilder :
        ITIdentifiedPoco<IDbQueryBuilder>,
        ITBdoScoped<IDbQueryBuilder>
    {
        /// <summary>
        /// Builds the SQL text from the specified database query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="parameterMode">The display mode of parameters to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varElementSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        public string BuildQuery(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IBdoElementSet parameterSet = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);
    }
}
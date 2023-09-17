using BindOpen.Labs.Databases.Data;
using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Logging;
using BindOpen.System.Scoping;

namespace BindOpen.Labs.Databases.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial interface IDbQueryBuilder :
        IIdentified,
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
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varElementSet = null,
            IBdoLog log = null);
    }
}
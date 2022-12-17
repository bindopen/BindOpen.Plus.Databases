using System.Collections.Generic;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbCompositeQuery : IDbQuery
    {
        /// <summary>
        /// The queries of this instance.
        /// </summary>
        List<IDbQuery> Queries { get; set; }

        /// <summary>
        /// Sets the specified queries.
        /// </summary>
        /// <param name="queries">The queries to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbCompositeQuery WithQueries(params IDbQuery[] queries);
    }
}
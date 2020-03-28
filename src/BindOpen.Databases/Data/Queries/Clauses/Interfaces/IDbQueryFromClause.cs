using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromClause : IDbQueryClause
    {
        /// <summary>
        /// The statements of this instance.
        /// </summary>
        List<DbQueryFromStatement> Statements { get; set; }
    }
}
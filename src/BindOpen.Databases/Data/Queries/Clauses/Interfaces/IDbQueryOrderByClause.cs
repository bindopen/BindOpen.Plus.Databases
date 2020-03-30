using System.Collections.Generic;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryOrderByClause : IDbQueryClause
    {
        /// <summary>
        /// The statements of this instance.
        /// </summary>
        List<DbQueryOrderByStatement> Statements { get; set; }
    }
}
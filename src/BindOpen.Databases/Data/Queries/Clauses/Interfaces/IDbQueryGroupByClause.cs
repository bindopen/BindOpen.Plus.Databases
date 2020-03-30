using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryGroupByClause : IDbQueryClause
    {
        /// <summary>
        /// 
        /// </summary>
        List<DbField> Fields { get; set; }
    }
}
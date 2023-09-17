using System.Collections.Generic;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbStoredQuery : IDbQuery
    {
        /// <summary>
        /// The query of this instance.
        /// </summary>
        IDbQuery Query { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IDbStoredQuery WithQuery(IDbStoredQuery query);

        /// <summary>
        /// The SQL query texts of this instance depending on connector unique.
        /// </summary>
        Dictionary<string, string> QueryTexts { get; set; }
    }
}
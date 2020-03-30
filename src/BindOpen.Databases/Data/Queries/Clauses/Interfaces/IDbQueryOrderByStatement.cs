using BindOpen.Data.Common;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryOrderByStatement : IDbQueryStatement
    {
        /// <summary>
        /// 
        /// </summary>
        DbField Field { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataSortingMode Sorting { get; set; }
    }
}
using BindOpen.Data.Expression;
using BindOpen.Data.Items;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryItem : IDataItem
    {
        /// <summary>
        /// The value to consider.
        /// </summary>
        DataExpression Expression { get; set; }
    }
}
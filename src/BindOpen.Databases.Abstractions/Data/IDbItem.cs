using BindOpen.Framework.MetaData.Expression;
using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbItem : IDataItem
    {
        IDataExpression Expression { get; set; }

        string ToString();
    }
}
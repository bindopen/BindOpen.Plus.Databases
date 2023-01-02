using BindOpen.Data.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbItem : IBdoItem
    {
        IBdoExpression Expression { get; set; }

        string ToString();
    }
}
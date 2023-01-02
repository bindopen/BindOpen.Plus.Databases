using BindOpen.Data.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITDbItem<T> : IDbItem where T : IDbItem
    {
        T WithExpression(IBdoExpression expression);
    }
}
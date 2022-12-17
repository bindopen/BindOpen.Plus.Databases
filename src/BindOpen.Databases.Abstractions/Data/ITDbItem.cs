using BindOpen.Framework.MetaData.Expression;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITDbItem<T> : IDbItem where T : IDbItem
    {
        T WithExpression(IDataExpression expression);
    }
}
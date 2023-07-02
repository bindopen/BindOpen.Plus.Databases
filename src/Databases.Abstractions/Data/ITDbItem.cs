using BindOpen.System.Data;

namespace BindOpen.Labs.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITDbObject<T> : IDbObject where T : IDbObject
    {
        T WithExpression(IBdoExpression expression);
    }
}
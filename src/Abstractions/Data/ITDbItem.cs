using BindOpen.Kernel.Data;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITDbObject<T> : IDbObject where T : IDbObject
    {
        T WithExpression(IBdoExpression expression);
    }
}
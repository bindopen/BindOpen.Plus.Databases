using BindOpen.Kernel.Data;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbObject : IBdoObject
    {
        IBdoExpression Expression { get; set; }

        string ToString();
    }
}
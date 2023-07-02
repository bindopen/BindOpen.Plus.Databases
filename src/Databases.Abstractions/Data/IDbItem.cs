using BindOpen.System.Data;

namespace BindOpen.Labs.Databases.Data
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
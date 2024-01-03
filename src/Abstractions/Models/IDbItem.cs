using BindOpen.Data;

namespace BindOpen.Databases.Models
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
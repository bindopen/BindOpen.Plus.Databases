using BindOpen.Data;
using BindOpen.Scoping.Entities;

namespace BindOpen.Databases.Relational
{
    public interface IDbField : IBdoEntity, IDbObject, INamed
    {
        IBdoExpression Value { get; set; }

        string Alias { get; set; }

        string DataModule { get; set; }

        string DataTable { get; set; }

        string DataTableAlias { get; set; }

        string Schema { get; set; }

        bool IsAll { get; set; }

        bool IsKey { get; set; }

        bool IsForeignKey { get; set; }

        int? Size { get; set; }

        DataValueTypes ValueType { get; set; }

        IDbQuery Query { get; set; }

        string GetName();
    }
}
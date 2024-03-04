using BindOpen.Data;
using BindOpen.Scoping.Entities;

namespace BindOpen.Databases.Relational
{
    public interface IDbTable : IBdoEntity, IDbObject, INamed
    {
        string Alias { get; set; }

        string Schema { get; set; }

        string DataModule { get; set; }
    }
}
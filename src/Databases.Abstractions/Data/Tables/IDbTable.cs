using BindOpen.System.Data;
using BindOpen.System.Scoping.Entities;

namespace BindOpen.Labs.Databases.Data
{
    public interface IDbTable :
        IBdoEntity,
        ITDbObject<IDbTable>, INamed
    {
        string Alias { get; set; }

        IDbTable WithAlias(string alias);

        string Schema { get; set; }

        IDbTable WithSchema(string schema);

        string DataModule { get; set; }

        IDbTable WithDataModule(string dataModule);
    }
}
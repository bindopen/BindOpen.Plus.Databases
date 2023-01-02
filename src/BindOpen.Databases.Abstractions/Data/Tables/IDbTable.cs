using BindOpen.Data.Items;
using BindOpen.Extensions.Modeling;

namespace BindOpen.Databases.Data
{
    public interface IDbTable : IBdoCarrier, ITDbItem<IDbTable>, ITNamedPoco<IDbTable>
    {
        string Alias { get; set; }

        IDbTable WithAlias(string alias);

        string Schema { get; set; }

        IDbTable WithSchema(string schema);

        string DataModule { get; set; }

        IDbTable WithDataModule(string dataModule);
    }
}
using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Extensions.Modeling;

namespace BindOpen.Databases.Data
{
    public interface IDbField : IBdoCarrier, ITDbItem<IDbField>, ITNamedPoco<IDbField>
    {
        IBdoExpression Value { get; set; }

        IDbField SetValue(IBdoExpression value);

        string Alias { get; }

        IDbField WithAlias(string alias);

        string DataModule { get; set; }

        IDbField WithDataModule(string dataModule);

        string DataTable { get; set; }

        IDbField WithDataTable(string dataTable);

        string DataTableAlias { get; set; }

        IDbField WithDataTableAlias(string dataTableAlias);

        string Schema { get; set; }

        IDbField WithSchema(string schema);

        IDbField AsNull();

        bool IsAll { get; set; }

        IDbField AsAll(bool isAll = false);

        bool IsKey { get; set; }

        IDbField AsKey(bool isKey = false);

        bool IsForeignKey { get; set; }

        IDbField AsForeignKey(bool isForeignKey = false);

        IDbQuery Query { get; set; }

        IDbField WithQuery(IDbQuery query);

        int? Size { get; set; }

        IDbField WithSize(int? size);

        DataValueTypes ValueType { get; set; }

        IDbField WithValueType(DataValueTypes valueType);

        string GetName();
    }
}
namespace BindOpen.Data.Queries
{
    public interface IDbQueryUnionClause : IDbQueryClause
    {
        DbQueryUnionKind Kind { get; set; }
        IDbSingleQuery Query { get; set; }
    }
}
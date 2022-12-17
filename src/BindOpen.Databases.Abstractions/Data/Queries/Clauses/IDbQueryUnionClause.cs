namespace BindOpen.Databases.Data
{
    public interface IDbQueryUnionClause :
        ITDbItem<IDbQueryUnionClause>,
        IDbQueryClause
    {
        DbQueryUnionKind Kind { get; set; }

        /// <summary>
        /// Sets the kind of this instance.
        /// </summary>
        IDbQueryUnionClause WithKind(DbQueryUnionKind kind);

        IDbSingleQuery Query { get; set; }

        /// <summary>
        /// Sets the kind of this instance.
        /// </summary>
        IDbQueryUnionClause WithQuery(IDbSingleQuery query);
    }
}
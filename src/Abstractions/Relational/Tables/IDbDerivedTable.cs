namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents the Join table of a database data query.
    /// </summary>
    public interface IDbDerivedTable : IDbTable
    {
        /// <summary>
        /// The query of this instance.
        /// </summary>
        IDbQuery Query { get; set; }
    }
}
using BindOpen.Data;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents the Join table of a database data query.
    /// </summary>
    public interface IDbJoinedTable : IDbTable
    {
        /// <summary>
        /// The kind of join of this instance.
        /// </summary>
        DbQueryJoinKind Kind { get; set; }

        /// <summary>
        /// The table of this instance.
        /// </summary>
        IDbTable Table { get; set; }

        IBdoExpression Condition { get; set; }
    }
}
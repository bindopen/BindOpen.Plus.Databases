using BindOpen.Data;

namespace BindOpen.Plus.Databases.Models
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
        /// 
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        IDbJoinedTable WithKind(DbQueryJoinKind kind);

        /// <summary>
        /// The table of this instance.
        /// </summary>
        IDbTable Table { get; set; }

        IDbJoinedTable WithTable(IDbTable table);

        /// <summary>
        /// The condition of this instance.
        /// </summary>
        IBdoExpression Condition { get; set; }

        /// <summary>
        /// Sets the specified condition.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbJoinedTable WithCondition(IBdoExpression condition);
    }
}
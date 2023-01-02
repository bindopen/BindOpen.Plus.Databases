using BindOpen.Data.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents the Join table of a database data query.
    /// </summary>
    public class DbJoinedTable : DbTable, IDbJoinedTable
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryJoinedTable class.
        /// </summary>
        public DbJoinedTable()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbQueryJoinedTable class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbJoinedTable(DbQueryJoinKind kind, DbTable table)
        {
            Kind = kind;
            Table = table;
        }

        #endregion

        // ------------------------------------------
        // IDbJoinedTable Implementation
        // ------------------------------------------

        #region IDbJoinedTable

        /// <summary>
        /// The kind of join of this instance.
        /// </summary>
        public DbQueryJoinKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public IDbJoinedTable WithKind(DbQueryJoinKind kind)
        {
            Kind = kind;
            return this;
        }

        /// <summary>
        /// The table of this instance.
        /// </summary>
        public IDbTable Table { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public IDbJoinedTable WithTable(IDbTable table)
        {
            Table = table;
            return this;
        }

        /// <summary>
        /// The condition of this instance.
        /// </summary>
        public IBdoExpression Condition { get; set; }

        /// <summary>
        /// Sets the specified condition.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbJoinedTable WithCondition(IBdoExpression condition)
        {
            Condition = condition;
            return this;
        }

        /// <summary>
        /// Sets the specified condition.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbJoinedTable WithCondition(string condition)
            => WithCondition(condition?.AsExpression(BdoExpressionKind.Script));

        #endregion

        // ------------------------------------------
        // IBdoItem Implementation
        // ------------------------------------------

        #region IBdoItem

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone(areas) as DbJoinedTable;
            clone.Table = Table?.Clone<DbTable>();
            clone.Condition = Condition?.Clone<BdoExpression>();

            return clone;
        }

        #endregion
    }
}
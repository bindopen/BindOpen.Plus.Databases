using BindOpen.Data;

namespace BindOpen.Databases.Relational
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
        /// The table of this instance.
        /// </summary>
        public IDbTable Table { get; set; }

        /// <summary>
        /// The condition of this instance.
        /// </summary>
        public IBdoExpression Condition { get; set; }

        #endregion

        // ------------------------------------------
        // IBdoObject Implementation
        // ------------------------------------------

        #region IBdoObject

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone()
        {
            var clone = base.Clone() as DbJoinedTable;
            clone.Table = Table?.Clone<DbTable>();
            clone.Condition = Condition?.Clone<BdoExpression>();

            return clone;
        }

        #endregion
    }
}
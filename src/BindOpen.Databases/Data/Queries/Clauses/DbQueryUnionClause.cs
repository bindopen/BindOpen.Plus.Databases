namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a union clause of a database data query.
    /// </summary>
    public class DbQueryUnionClause : DbQueryItem, IDbQueryUnionClause
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>p
        /// Type of this instance.
        /// </summary>
        public DbQueryUnionKind Kind { get; set; }

        /// <summary>
        /// Data query of this instance.
        /// </summary>
        public IDbSingleQuery Query { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryUnionClause class.
        /// </summary>
        public DbQueryUnionClause()
        {
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone()
        {
            var clone = base.Clone() as DbDerivedTable;
            clone.Query = Query.Clone<DbQuery>();

            return clone;
        }

        #endregion
    }
}
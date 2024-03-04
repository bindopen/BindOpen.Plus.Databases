namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents the Join table of a database data query.
    /// </summary>
    public class DbDerivedTable : DbTable, IDbDerivedTable
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryDerivedTable class.
        /// </summary>
        public DbDerivedTable()
        {
        }

        #endregion

        // ------------------------------------------
        // IDerivedTable Implementation
        // ------------------------------------------

        #region IDerivedTable

        /// <summary>
        /// The query of this instance.
        /// </summary>
        public IDbQuery Query { get; set; }

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
            var clone = base.Clone<IDbDerivedTable>();
            clone.Query = Query?.Clone<IDbQuery>();

            return clone;
        }

        #endregion
    }
}
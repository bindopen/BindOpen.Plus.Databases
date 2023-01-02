namespace BindOpen.Databases.Data
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IDbDerivedTable WithQuery(IDbQuery query)
        {
            Query = query;
            return this;
        }

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
            var clone = base.Clone<IDbDerivedTable>(areas);
            clone.Query = Query?.Clone<IDbQuery>();

            return clone;
        }

        #endregion
    }
}
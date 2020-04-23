namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the Having clause of a database data query.
    /// </summary>
    public class DbQueryHavingClause : DbQueryItem, IDbQueryHavingClause
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryHavingClause class.
        /// </summary>
        public DbQueryHavingClause()
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
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone() as DbQueryHavingClause;

            return clone;
        }

        #endregion
    }
}
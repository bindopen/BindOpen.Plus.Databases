using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents the Join table of a database data query.
    /// </summary>
    public class DbDerivedTable : DbTable
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The query of this instance.
        /// </summary>
        public DbQuery Query { get; set; }

        #endregion

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
            clone.Query = Query?.Clone<DbQuery>();

            return clone;
        }

        #endregion
    }
}
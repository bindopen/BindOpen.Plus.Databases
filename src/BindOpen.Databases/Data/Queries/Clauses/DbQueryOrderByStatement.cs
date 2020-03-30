using BindOpen.Data.Common;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the Order-By clause of a database data query.
    /// </summary>
    public class DbQueryOrderByStatement : DbQueryItem, IDbQueryOrderByStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The sorting order of this instance.
        /// </summary>
        public DataSortingMode Sorting { get; set; }

        /// <summary>
        /// The field of this instance.
        /// </summary>
        public DbField Field { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryOrderByStatement class.
        /// </summary>
        public DbQueryOrderByStatement()
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
            var clone = base.Clone() as DbQueryOrderByStatement;
            clone.Field = Field?.Clone<DbField>();

            return clone;
        }

        #endregion
    }
}
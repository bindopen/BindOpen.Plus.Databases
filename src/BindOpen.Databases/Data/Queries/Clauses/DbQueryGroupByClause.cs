using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Queries
{

    /// <summary>
    /// This class represents the GroupBy clause of a database data query.
    /// </summary>
    public class DbQueryGroupByClause : DbQueryItem, IDbQueryGroupByClause
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        public List<DbField> Fields { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryGroupByClause class.
        /// </summary>
        public DbQueryGroupByClause()
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
            var clone = base.Clone() as DbQueryGroupByClause;
            clone.Fields = Fields?.Select(p => p.Clone<DbField>()).ToList();

            return clone;
        }

        #endregion
    }
}
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents the Order-By clause of a database data query.
    /// </summary>
    public class DbQueryOrderByClause : DbQueryItem, IDbQueryOrderByClause
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The statements of this instance.
        /// </summary>
        public List<DbQueryOrderByStatement> Statements { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryOrderByClause class.
        /// </summary>
        public DbQueryOrderByClause()
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
            var clone = base.Clone() as DbQueryOrderByClause;
            clone.Statements = Statements?.Select(p => p.Clone<DbQueryOrderByStatement>()).ToList();

            return clone;
        }

        #endregion
    }
}
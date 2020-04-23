using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the Where clause of a database data query.
    /// </summary>
    public class DbQueryWhereClause : DbQueryItem, IDbQueryWhereClause
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public List<DbField> IdFields { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryWhereClause class.
        /// </summary>
        public DbQueryWhereClause()
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
            var clone = base.Clone() as DbQueryWhereClause;
            clone.IdFields = IdFields?.Select(p => p.Clone<DbField>()).ToList();

            return clone;
        }

        #endregion
    }
}
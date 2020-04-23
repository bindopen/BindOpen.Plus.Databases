using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the From clause of a database data query.
    /// </summary>
    public class DbQueryFromStatement : DbQueryItem, IDbQueryFromStatement
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The tables of this instance.
        /// </summary>
        public List<DbTable> Tables { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryFromStatement class.
        /// </summary>
        public DbQueryFromStatement()
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
            var clone = base.Clone() as DbQueryFromStatement;
            clone.Tables = Tables?.Select(p => p.Clone<DbTable>()).ToList();

            return clone;
        }

        #endregion
    }
}
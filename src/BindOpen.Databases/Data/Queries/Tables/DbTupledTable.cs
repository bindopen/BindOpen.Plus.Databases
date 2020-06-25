using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the tupled table.
    /// </summary>
    public class DbTupledTable : DbTable
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The tuples of this instance.
        /// </summary>
        public List<DbTuple> Tuples { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbTupledTable class.
        /// </summary>
        public DbTupledTable()
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
            var clone = base.Clone(areas) as DbTupledTable;
            clone.Tuples = Tuples?.Select(p => p.Clone<DbTuple>()).ToList();

            return clone;
        }

        #endregion
    }
}
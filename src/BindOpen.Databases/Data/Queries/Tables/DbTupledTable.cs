using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Queries
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
        /// <param name="tuples">The tuples to consider.</param>
        public DbTupledTable(params DbTuple[] tuples)
        {
            Tuples = tuples?.ToList();
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
            var clone = base.Clone() as DbTupledTable;
            clone.Tuples = Tuples?.Select(p => p.Clone<DbTuple>()).ToList();

            return clone;
        }

        #endregion
    }
}
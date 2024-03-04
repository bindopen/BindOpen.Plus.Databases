using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents the tupled table.
    /// </summary>
    public class DbTupledTable : DbTable, IDbTupledTable
    {
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
        // IDbTupledTable Implementation
        // ------------------------------------------

        #region IDbTupledTable

        /// <summary>
        /// The tuples of this instance.
        /// </summary>
        public List<IDbTuple> Tuples { get; set; }

        #endregion

        // ------------------------------------------
        // IBdoObject Implementation
        // ------------------------------------------

        #region IBdoObject

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone()
        {
            var clone = base.Clone<IDbTupledTable>();
            clone.Tuples = Tuples?.Select(p => p.Clone<IDbTuple>()).ToList();

            return clone;
        }

        #endregion
    }
}
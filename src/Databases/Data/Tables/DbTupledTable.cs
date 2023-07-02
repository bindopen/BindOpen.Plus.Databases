using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Labs.Databases.Data
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuples"></param>
        /// <returns></returns>
        public IDbTupledTable WithTuples(params IDbTuple[] tuples)
        {
            Tuples = tuples?.ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuples"></param>
        /// <returns></returns>
        public IDbTupledTable AddTuples(params IDbTuple[] tuples)
        {
            Tuples ??= new List<IDbTuple>();
            Tuples.AddRange(tuples);
            return this;
        }

        #endregion

        // ------------------------------------------
        // IBdoObject Implementation
        // ------------------------------------------

        #region IBdoObject

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone<IDbTupledTable>(areas);
            clone.Tuples = Tuples?.Select(p => p.Clone<IDbTuple>()).ToList();

            return clone;
        }

        #endregion
    }
}
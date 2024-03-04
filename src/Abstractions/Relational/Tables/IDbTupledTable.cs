using System.Collections.Generic;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents the tupled table.
    /// </summary>
    public interface IDbTupledTable : IDbTable
    {
        /// <summary>
        /// The tuples of this instance.
        /// </summary>
        public List<IDbTuple> Tuples { get; set; }
    }
}
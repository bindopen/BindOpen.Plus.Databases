using System.Collections.Generic;

namespace BindOpen.Databases.Models
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuples"></param>
        /// <returns></returns>
        IDbTupledTable WithTuples(params IDbTuple[] tuples);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuples"></param>
        /// <returns></returns>
        IDbTupledTable AddTuples(params IDbTuple[] tuples);
    }
}
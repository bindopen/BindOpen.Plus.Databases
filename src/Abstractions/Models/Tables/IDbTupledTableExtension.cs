using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents an extension of the IDbTableExtension enumeration.
    /// </summary>
    public static partial class IDbTupledTableExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuples"></param>
        /// <returns></returns>
        public static T WithTuples<T>(this T table, params IDbTuple[] tuples)
            where T : IDbTupledTable
        {
            if (table != null)
            {
                table.Tuples = tuples?.ToList();
            }

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuples"></param>
        /// <returns></returns>
        public static T AddTuples<T>(this T table, params IDbTuple[] tuples)
            where T : IDbTupledTable
        {
            if (table != null)
            {
                table.Tuples ??= new List<IDbTuple>();
                table.Tuples.AddRange(tuples);
            }

            return table;
        }
    }
}

using System;
using System.Linq;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents an extension of the IDbFieldExtension enumeration.
    /// </summary>
    public static class IDbTupleExtension
    {
        public static T WithFields<T>(this T tuple, params IDbField[] fields)
            where T : IDbTuple
        {
            if (tuple != null)
            {
                tuple.Fields = fields?.ToList();
            }

            return tuple;
        }

        public static T AddFields<T>(this T tuple, params IDbField[] fields)
            where T : IDbTuple
        {
            if (tuple != null)
            {
                // first we remove common fields
                tuple.Fields.RemoveAll(q =>
                    fields.Any(
                        p => (q.Alias ?? q.Name).Equals(p.Alias ?? p.Name, StringComparison.OrdinalIgnoreCase)));

                tuple.Fields.AddRange(fields);
            }

            return tuple;
        }
    }
}

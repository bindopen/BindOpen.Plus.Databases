using System;
using System.Linq;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents the extension for database field array.
    /// </summary>
    public static partial class IDbFieldExtension
    {
        /// <summary>
        /// Adds the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <param name="newfields">The new fields to exclude.</param>
        public static IDbField[] Adding(
            this IDbField[] fields,
            params IDbField[] newfields)
        {
            var fieldsList = fields.Excluding(newfields).ToList();

            fieldsList.AddRange(newfields);

            return fieldsList.ToArray();
        }

        /// <summary>
        /// Excludes the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <param name="excludingfields">The fields to exclude.</param>
        /// <returns></returns>
        public static IDbField[] Excluding(
            this IDbField[] fields,
            params IDbField[] excludingfields)
        {
            var fieldsList = fields?.ToList();

            // first we remove common fields
            fieldsList.RemoveAll(q =>
                excludingfields.Any(
                    p => (q.Alias ?? q.Name).Equals(p.Alias ?? p.Name, StringComparison.OrdinalIgnoreCase)));

            return fieldsList.ToArray();
        }
    }
}
using BindOpen.Extensions.Carriers;
using System;
using System.Linq;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the extension for database field array.
    /// </summary>
    public static class DbFieldArrayExtension
    {
        /// <summary>
        /// Adds the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <param name="newfields">The new fields to exclude.</param>
        public static DbField[] Adding(
            this DbField[] fields,
            params DbField[] newfields)
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
        public static DbField[] Excluding(
            this DbField[] fields,
            params DbField[] excludingfields)
        {
            var fieldsList = fields?.ToList();

            // first we remove common fields
            fieldsList.RemoveAll(q =>
                excludingfields.Any(
                    p => (q.Alias ?? q.Name).Equals((p.Alias ?? p.Name), StringComparison.OrdinalIgnoreCase)));

            return fieldsList.ToArray();
        }
    }
}
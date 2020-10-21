using BindOpen.Data.Items;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the database tuple.
    /// </summary>
    public class DbTuple : DataItem, IDbTuple
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The fields of this instance.
        /// </summary>
        public List<DbField> Fields { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbTuple class.
        /// </summary>
        public DbTuple()
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
            var clone = base.Clone(areas) as DbTuple;
            clone.Fields = Fields?.Select(p => p.Clone<DbField>()).ToList();

            return clone;
        }

        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        public IDbTuple AddFields(params DbField[] fields)
        {
            // first we remove common fields
            Fields.RemoveAll(q =>
                fields.Any(
                    p => (q.Alias ?? q.Name).Equals((p.Alias ?? p.Name), StringComparison.OrdinalIgnoreCase)));

            Fields.AddRange(fields);

            return this;
        }

        #endregion

    }
}
using BindOpen.Data.Items;
using BindOpen.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents the database tuple.
    /// </summary>
    public class DbTuple : BdoItem, IDbTuple
    {
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
        // ITDbItem Implementation
        // ------------------------------------------

        #region ITDbItem

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        public IBdoExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IDbTuple WithExpression(IBdoExpression exp)
        {
            Expression = exp;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbTuple Implementation
        // ------------------------------------------

        #region IDbTuple

        /// <summary>
        /// The fields of this instance.
        /// </summary>
        public List<IDbField> Fields { get; set; }

        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        public IDbTuple WithFields(params IDbField[] fields)
        {
            Fields = fields?.ToList();
            return this;
        }

        /// <summary>
        /// Adds the specified fields.
        /// </summary>
        public IDbTuple AddFields(params IDbField[] fields)
        {
            // first we remove common fields
            Fields.RemoveAll(q =>
                fields.Any(
                    p => (q.Alias ?? q.Name).Equals(p.Alias ?? p.Name, StringComparison.OrdinalIgnoreCase)));

            Fields.AddRange(fields);

            return this;
        }

        #endregion

        // ------------------------------------------
        // IBdoItem Implementation
        // ------------------------------------------

        #region IBdoItem

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone<IDbTuple>(areas);
            clone.WithFields(Fields?.Select(p => p.Clone<IDbField>()).ToArray());

            return clone;
        }

        #endregion
    }
}
using BindOpen.Data;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents the database tuple.
    /// </summary>
    public class DbTuple : BdoObject, IDbTuple
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
        // ITDbObject Implementation
        // ------------------------------------------

        #region ITDbObject

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        public IBdoExpression Expression { get; set; }

        #endregion

        // ------------------------------------------
        // IDbTuple Implementation
        // ------------------------------------------

        #region IDbTuple

        /// <summary>
        /// The fields of this instance.
        /// </summary>
        public List<IDbField> Fields { get; set; }

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
            var clone = Clone<IDbTuple>();
            clone.WithFields(Fields?.Select(p => p.Clone<IDbField>()).ToArray());

            return clone;
        }

        #endregion
    }
}
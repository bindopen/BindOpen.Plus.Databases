using BindOpen.Data;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents the Where clause of a database data query.
    /// </summary>
    public class DbQueryWhereClause : BdoObject, IDbQueryWhereClause
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryWhereClause class.
        /// </summary>
        public DbQueryWhereClause()
        {
        }

        #endregion

        // ------------------------------------------
        // IDbObject Implementation
        // ------------------------------------------

        #region IDbObject

        /// <summary>
        /// 
        /// </summary>
        public IBdoExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IDbQueryWhereClause WithExpression(IBdoExpression expression)
        {
            Expression = expression;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbQueryWhereClause Implementation
        // ------------------------------------------

        #region IDbQueryWhereClause

        /// <summary>
        /// 
        /// </summary>
        public List<IDbField> IdFields { get; set; }

        public IDbQueryWhereClause WithIdFields(params IDbField[] fields)
        {
            IdFields = fields.ToList();
            return this;
        }

        public IDbQueryWhereClause AddIdFields(params IDbField[] fields)
        {
            IdFields ??= new List<IDbField>();
            IdFields.AddRange(fields);
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
        public override object Clone()
        {
            var clone = base.Clone<IDbQueryWhereClause>();
            clone.IdFields = IdFields?.Select(p => p.Clone<IDbField>()).ToList();

            return clone;
        }

        #endregion
    }
}
using BindOpen.System.Data;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Labs.Databases.Data
{

    /// <summary>
    /// This class represents the GroupBy clause of a database data query.
    /// </summary>
    public class DbQueryGroupByClause : BdoObject, IDbQueryGroupByClause
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryGroupByClause class.
        /// </summary>
        public DbQueryGroupByClause()
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
        public IDbQueryGroupByClause WithExpression(IBdoExpression expression)
        {
            Expression = expression;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbQueryGroupByClause Implementation
        // ------------------------------------------

        #region IDbQueryGroupByClause

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        public List<IDbField> Fields { get; set; }

        public IDbQueryGroupByClause WithFields(params IDbField[] fields)
        {
            Fields = fields.ToList();
            return this;
        }

        public IDbQueryGroupByClause AddFields(params IDbField[] fields)
        {
            Fields ??= new List<IDbField>();
            Fields.AddRange(fields);
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
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone<IDbQueryGroupByClause>(areas);
            clone.Fields = Fields?.Select(p => p.Clone<IDbField>()).ToList();

            return clone;
        }

        #endregion
    }
}
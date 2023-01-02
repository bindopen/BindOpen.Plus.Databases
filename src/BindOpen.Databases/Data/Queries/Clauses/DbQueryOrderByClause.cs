using BindOpen.Data.Items;
using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents the Order-By clause of a database data query.
    /// </summary>
    public class DbQueryOrderByClause : BdoItem, IDbQueryOrderByClause
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryOrderByClause class.
        /// </summary>
        public DbQueryOrderByClause()
        {
        }

        #endregion

        // ------------------------------------------
        // IDbQueryOrderByClause Implementation
        // ------------------------------------------

        #region IDbQueryOrderByClause

        /// <summary>
        /// The statements of this instance.
        /// </summary>
        public List<IDbQueryOrderByStatement> Statements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        public IDbQueryOrderByClause WithStatements(params IDbQueryOrderByStatement[] statements)
        {
            Statements = statements.ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        public IDbQueryOrderByClause AddStatements(params IDbQueryOrderByStatement[] statements)
        {
            Statements ??= new List<IDbQueryOrderByStatement>();
            Statements.AddRange(statements);
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbItem Implementation
        // ------------------------------------------

        #region IDbItem

        /// <summary>
        /// 
        /// </summary>
        public IBdoExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IDbQueryOrderByClause WithExpression(IBdoExpression expression)
        {
            Expression = expression;
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
            var clone = base.Clone<IDbQueryOrderByClause>(areas);
            clone.Statements = Statements?.Select(p => p.Clone<IDbQueryOrderByStatement>()).ToList();

            return clone;
        }

        #endregion
    }
}
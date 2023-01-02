using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents the From clause of a database data query.
    /// </summary>
    public class DbQueryFromClause : BdoItem, IDbQueryFromClause
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryFromClause class.
        /// </summary>
        public DbQueryFromClause()
        {
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
        public IDbQueryFromClause WithExpression(IBdoExpression expression)
        {
            Expression = expression;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbQueryFromClause Implementation
        // ------------------------------------------

        #region IDbQueryFromClause

        /// <summary>
        /// The statements of this instance.
        /// </summary>
        public List<IDbQueryFromStatement> Statements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        public IDbQueryFromClause WithStatements(params IDbQueryFromStatement[] statements)
        {
            Statements = statements.ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statememnt"></param>
        /// <returns></returns>
        public IDbQueryFromClause AddStatements(params IDbQueryFromStatement[] statements)
        {
            Statements ??= new List<IDbQueryFromStatement>();
            Statements.AddRange(statements);
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
            var clone = base.Clone(areas) as DbQueryFromClause;
            clone.Statements = Statements?.Select(p => p.Clone<IDbQueryFromStatement>()).ToList();

            return clone;
        }

        #endregion
    }
}
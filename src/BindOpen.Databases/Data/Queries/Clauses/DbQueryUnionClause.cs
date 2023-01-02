using BindOpen.Data.Items;
using BindOpen.Data.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a union clause of a database data query.
    /// </summary>
    public class DbQueryUnionClause : BdoItem, IDbQueryUnionClause
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryUnionClause class.
        /// </summary>
        public DbQueryUnionClause()
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
        public IDbQueryUnionClause WithExpression(IBdoExpression expression)
        {
            Expression = expression;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbQueryUnionClause Implementation
        // ------------------------------------------

        #region IDbQueryUnionClause

        /// <summary>p
        /// Type of this instance.
        /// </summary>
        public DbQueryUnionKind Kind { get; set; }

        public IDbQueryUnionClause WithKind(DbQueryUnionKind kind)
        {
            Kind = kind;
            return this;
        }

        /// <summary>
        /// Data query of this instance.
        /// </summary>
        public IDbSingleQuery Query { get; set; }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IDbQueryUnionClause WithQuery(IDbSingleQuery query)
        {
            Query = query;
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
            var clone = base.Clone<IDbDerivedTable>(areas);
            clone.Query = Query.Clone<IDbQuery>();

            return clone;
        }

        #endregion
    }
}
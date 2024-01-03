using BindOpen.Data;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents a union clause of a database data query.
    /// </summary>
    public class DbQueryUnionClause : BdoObject, IDbQueryUnionClause
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
        // IBdoObject Implementation
        // ------------------------------------------

        #region IBdoObject

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone()
        {
            var clone = base.Clone<IDbDerivedTable>();
            clone.Query = Query.Clone<IDbQuery>();

            return clone;
        }

        #endregion
    }
}
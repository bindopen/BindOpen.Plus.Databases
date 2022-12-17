using BindOpen.Framework.MetaData.Expression;
using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a union clause of a database data query.
    /// </summary>
    public class DbQueryUnionClause : DataItem, IDbQueryUnionClause
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
        public IDataExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IDbQueryUnionClause WithExpression(IDataExpression expression)
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
        // IDataItem Implementation
        // ------------------------------------------

        #region IDataItem

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
using BindOpen.System.Data.Items;

namespace BindOpen.Labs.Databases.Data
{
    /// <summary>
    /// This class represents the Having clause of a database data query.
    /// </summary>
    public class DbQueryHavingClause : BdoObject, IDbQueryHavingClause
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryHavingClause class.
        /// </summary>
        public DbQueryHavingClause()
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
        public IDbQueryHavingClause WithExpression(IBdoExpression expression)
        {
            Expression = expression;
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
            var clone = base.Clone<IDbQueryHavingClause>(areas);

            return clone;
        }

        #endregion
    }
}
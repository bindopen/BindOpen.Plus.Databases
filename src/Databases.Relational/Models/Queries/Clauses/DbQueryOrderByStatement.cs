using BindOpen.Data;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents the Order-By clause of a database data query.
    /// </summary>
    public class DbQueryOrderByStatement : BdoObject, IDbQueryOrderByStatement
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryOrderByStatement class.
        /// </summary>
        public DbQueryOrderByStatement()
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
        public IDbQueryOrderByStatement WithExpression(IBdoExpression expression)
        {
            Expression = expression;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbQueryOrderByStatement Implementation
        // ------------------------------------------

        #region IDbQueryOrderByStatement

        /// <summary>
        /// The sorting order of this instance.
        /// </summary>
        public DataSortingModes Sorting { get; set; }

        public IDbQueryOrderByStatement WithSorting(DataSortingModes sorting)
        {
            Sorting = sorting;
            return this;
        }

        /// <summary>
        /// The field of this instance.
        /// </summary>
        public IDbField Field { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public IDbQueryOrderByStatement WithField(IDbField field)
        {
            Field = field;
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
            var clone = base.Clone() as DbQueryOrderByStatement;
            clone.Field = Field?.Clone<DbField>();

            return clone;
        }

        #endregion
    }
}
using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Expression;
using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents the Order-By clause of a database data query.
    /// </summary>
    public class DbQueryOrderByStatement : DataItem, IDbQueryOrderByStatement
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
        public IDbQueryOrderByStatement WithExpression(IDataExpression expression)
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
        // IDataItem Implementation
        // ------------------------------------------

        #region IDataItem

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone(areas) as DbQueryOrderByStatement;
            clone.Field = Field?.Clone<DbField>();

            return clone;
        }

        #endregion
    }
}
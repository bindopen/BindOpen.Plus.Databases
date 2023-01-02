using BindOpen.Data.Items;
using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents the From clause of a database data query.
    /// </summary>
    public class DbQueryFromStatement : BdoItem, IDbQueryFromStatement
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryFromStatement class.
        /// </summary>
        public DbQueryFromStatement()
        {
        }

        #endregion

        // ------------------------------------------
        // ITDbItem Implementation
        // ------------------------------------------

        #region ITDbItem

        /// <summary>
        /// 
        /// </summary>
        public IBdoExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IDbQueryFromStatement WithExpression(IBdoExpression expression)
        {
            Expression = expression;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbQueryFromStatement Implementation
        // ------------------------------------------

        #region IDbQueryFromStatement

        /// <summary>
        /// The tables of this instance.
        /// </summary>
        public List<IDbTable> Tables { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        public IDbQueryFromStatement WithTables(params IDbTable[] tables)
        {
            Tables = tables.ToList();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        public IDbQueryFromStatement AddTables(params IDbTable[] tables)
        {
            Tables ??= new List<IDbTable>();
            Tables.AddRange(tables.ToList());
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
            var clone = base.Clone<IDbQueryFromStatement>(areas);
            clone.Tables = Tables?.Select(p => p.Clone<IDbTable>()).ToList();

            return clone;
        }

        #endregion
    }
}
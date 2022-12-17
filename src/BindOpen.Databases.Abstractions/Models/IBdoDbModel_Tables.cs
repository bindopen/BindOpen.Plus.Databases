using BindOpen.Databases.Data;
using BindOpen.Framework.MetaData.Expression;

namespace BindOpen.Databases.Models
{
    public partial interface IBdoDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbTableModel TableModel(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbTable Table(string name, string alias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDbTable Table<T>(string alias = null);

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public IDbJoinedTable TableAsJoin(string name, DbQueryJoinKind kind, IDataExpression condition);

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public IDbJoinedTable TableAsJoin<T>(DbQueryJoinKind kind, IDataExpression condition);

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table1Alias">The alias of the table 1 to consider.</param>
        /// <param name="table2Alias">The alias of the table 2 to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public IDbJoinedTable TableAsJoin<T, T1, T2>(
            DbQueryJoinKind kind,
            string table1Alias = null, string table2Alias = null);
    }
}
using BindOpen.Data.Expression;
using BindOpen.Databases.Data.Queries;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Databases.Data.Models
{
    public partial interface IBdoDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbTableModel TableModel(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbTable Table(string name, string alias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DbTable Table<T>(string alias = null);

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbJoinedTable TableAsJoin(string name, DbQueryJoinKind kind, DataExpression condition);

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbJoinedTable TableAsJoin<T>(DbQueryJoinKind kind, DataExpression condition);

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table1Alias">The alias of the table 1 to consider.</param>
        /// <param name="table2Alias">The alias of the table 2 to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbJoinedTable TableAsJoin<T, T1, T2>(
            DbQueryJoinKind kind,
            string table1Alias = null, string table2Alias = null);
    }
}
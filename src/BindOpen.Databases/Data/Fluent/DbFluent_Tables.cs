using BindOpen.Framework.MetaData.Expression;
using System.Linq;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This static class represents a factory of data table.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a new instance of the DbTable class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModuleName">The name of the data module to consider.</param>
        public static DbTable Table(string name = null, string schema = null, string dataModuleName = null)
            => new DbTable()
            {
                Name = name,
                DataModule = dataModuleName,
                Schema = schema,
            };

        // Join --------------------------------

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <param name="expression">The expression to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public static DbJoinedTable TableAsJoin(DbQueryJoinKind kind, IDbTable table, IDataExpression expression)
        {
            var joinedTable = new DbJoinedTable() { Kind = kind, Table = table };
            joinedTable.WithCondition(expression);
            return joinedTable;
        }

        ///// <summary>
        ///// Creates a new joined table.
        ///// </summary>
        ///// <param name="kind">The kind to consider.</param>
        ///// <param name="table">The table to consider.</param>
        ///// <param name="conditionScript">The condition script to consider.</param>
        ///// <returns>Returns a new From statement.</returns>
        //public static DbJoinedTable TableAsJoin(DbQueryJoinKind kind, DbTable table, string conditionScript)
        //    => TableAsJoin(kind, table, conditionScript.CreateExpAsScript());

        // Derived --------------------------------

        /// <summary>
        /// Creates a new instance of the DbDerivedTable class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        public static DbDerivedTable TableAsQuery(IDbQuery query)
            => new DbDerivedTable() { Query = query };

        // Tupled --------------------------------

        /// <summary>
        /// Creates a new instance of the DbTupledTable class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        public static DbTupledTable TableAsTuples(params IDbTuple[] tuples)
            => new DbTupledTable() { Tuples = tuples?.Cast<IDbTuple>().ToList() };

        /// <summary>
        /// Creates a new instance of the DbTuple class.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        public static IDbTuple Tuple(params IDbField[] fields)
            => new DbTuple() { Fields = fields?.ToList() };
    }
}

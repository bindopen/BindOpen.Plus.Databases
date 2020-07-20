using BindOpen.Data.Expression;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        // Condition --------------------------------

        /// <summary>
        /// Creates a BDO script representing and Sql And condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static DataExpression And(params object[] conditions)
        {
            return DbFunction("sqlAnd", conditions).CreateExp(); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Or condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static DataExpression Or(params object[] conditions)
        {
            return DbFunction("sqlOr", conditions).CreateExp(); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static DataExpression Xor(params object[] conditions)
        {
            return DbFunction("sqlXOr", conditions).CreateExp(); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Not condition including the specified condition strings.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        public static DataExpression Not(object condition)
            => DbFunction("sqlNot", condition).CreateExp();

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static DataExpression Like(object param1, object param2)
            => DbFunction("sqlLike", param1, param2).CreateExp();
    }
}

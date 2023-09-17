using BindOpen.System.Data;

namespace BindOpen.Labs.Databases.Data
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        // Condition --------------------------------

        /// <summary>
        /// Creates a BDO script representing and Sql If.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="thenResult">The then-result to consider.</param>
        /// <param name="elseResult">The else-result to consider.</param>
        public static BdoExpression If(object condition, object thenResult, object elseResult)
        {
            return DbFunction("sqlIf", condition, thenResult, elseResult).AsExpression();
        }

        /// <summary>
        /// Creates a BDO script representing and Sql And condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static BdoExpression And(params object[] conditions)
        {
            return DbFunction("sqlAnd", conditions).AsExpression(); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Or condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static BdoExpression Or(params object[] conditions)
        {
            return DbFunction("sqlOr", conditions).AsExpression(); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static BdoExpression Xor(params object[] conditions)
        {
            return DbFunction("sqlXOr", conditions).AsExpression(); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Not condition including the specified condition strings.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        public static BdoExpression Not(object condition)
            => DbFunction("sqlNot", condition).AsExpression();

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static BdoExpression Like(object param1, object param2)
            => DbFunction("sqlLike", param1, param2).AsExpression();
    }
}

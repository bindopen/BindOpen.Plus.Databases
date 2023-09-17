using BindOpen.Kernel.Scoping.Script;

namespace BindOpen.Plus.Databases.Data
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
        public static BdoScriptword If(object condition, object thenResult, object elseResult)
        {
            return DbFunction("sqlIf", condition, thenResult, elseResult);
        }

        /// <summary>
        /// Creates a BDO script representing and Sql And condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static BdoScriptword And(params object[] conditions)
        {
            return DbFunction("sqlAnd", conditions); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Or condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static BdoScriptword Or(params object[] conditions)
        {
            return DbFunction("sqlOr", conditions); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static BdoScriptword Xor(params object[] conditions)
        {
            return DbFunction("sqlXOr", conditions); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Not condition including the specified condition strings.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        public static BdoScriptword Not(object condition)
            => DbFunction("sqlNot", condition);

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static BdoScriptword Like(object param1, object param2)
            => DbFunction("sqlLike", param1, param2);
    }
}

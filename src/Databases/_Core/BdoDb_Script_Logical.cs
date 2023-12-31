using BindOpen.Scoping.Script;

namespace BindOpen.Plus.Databases
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class BdoDb
    {
        // Condition --------------------------------

        /// <summary>
        /// Creates a BDO script representing and Sql If.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="thenResult">The then-result to consider.</param>
        /// <param name="elseResult">The else-result to consider.</param>
        public static IBdoScriptword If(object condition, object thenResult, object elseResult)
        {
            return DbFunction("sqlIf", condition, thenResult, elseResult);
        }

        /// <summary>
        /// Creates a BDO script representing and Sql And condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static IBdoScriptword And(params object[] conditions)
        {
            return DbFunction("sqlAnd", conditions); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Or condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static IBdoScriptword Or(params object[] conditions)
        {
            return DbFunction("sqlOr", conditions); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static IBdoScriptword Xor(params object[] conditions)
        {
            return DbFunction("sqlXOr", conditions); ;
        }

        /// <summary>
        /// Creates a BDO script representing and Sql Not condition including the specified condition strings.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        public static IBdoScriptword Not(object condition)
            => DbFunction("sqlNot", condition);

        /// <summary>
        /// Creates a BDO script representing and Sql Xor condition including the specified condition strings.
        /// </summary>
        /// <param name="conditions">The conditions to consider.</param>
        public static IBdoScriptword Like(object param1, object param2)
            => DbFunction("sqlLike", param1, param2);
    }
}

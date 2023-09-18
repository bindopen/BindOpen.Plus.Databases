using BindOpen.Kernel.Scoping.Script;

namespace BindOpen.Plus.Databases
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class BdoDb
    {
        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static BdoScriptword Text(object param1)
            => BdoScript.Function("sqlText", param1);

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static BdoScriptword UpperCase(object param1)
            => DbFunction("sqlUCase", param1);

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static BdoScriptword LowerCase(object param1)
            => DbFunction("sqlLCase", param1);

        /// <summary>
        /// Gets the Sql contenation of the specified object.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static BdoScriptword Concat(params object[] values)
            => DbFunction("sqlConcat", values);

        /// <summary>
        /// Gets the Sql string contenation of the specified object.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static BdoScriptword StringConcat(params object[] values)
            => DbFunction("sqlStringConcat", values);

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter 1 to consider.</param>
        /// <param name="param2">The parameter 2 to consider.</param>
        /// <param name="param3">The parameter 3 to consider.</param>
        public static BdoScriptword LeftPadding(object param1, object param2, object param3)
            => DbFunction("sqlLPad", param1, param2, param3);

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter 1 to consider.</param>
        /// <param name="param2">The parameter 2 to consider.</param>
        /// <param name="param3">The parameter 3 to consider.</param>
        public static BdoScriptword RightPadding(object param1, object param2, object param3)
            => DbFunction("sqlRPad", param1, param2, param3);
    }
}

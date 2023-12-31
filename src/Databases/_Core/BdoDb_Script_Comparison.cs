using BindOpen.Scoping.Script;

namespace BindOpen.Plus.Databases
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class BdoDb
    {
        // Comparison

        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static IBdoScriptword Eq(object value1, object value2)
            => DbFunction("sqlEq", value1, value2);

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static IBdoScriptword Diff(object value1, object value2)
            => DbFunction("sqlDiff", value1, value2);

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static IBdoScriptword Gt(object value1, object value2)
            => DbFunction("sqlGt", value1, value2);

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static IBdoScriptword Gte(object value1, object value2)
            => DbFunction("sqlGte", value1, value2);

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static IBdoScriptword Lt(object value1, object value2)
            => DbFunction("sqlLt", value1, value2);

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static IBdoScriptword Lte(object value1, object value2)
            => DbFunction("sqlLte", value1, value2);

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted object value.</returns>
        public static IBdoScriptword IsNull(object value1)
            => DbFunction("sqlIsNull", value1);

        /// <summary>
        /// Evaluates the script word $SQLIFNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static IBdoScriptword IfNull(object value1, object value2)
            => DbFunction("sqlIfNull", value1, value2);

        /// <summary>
        /// Evaluates the script word $SQLEXISTS.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The interpreted object value.</returns>
        public static IBdoScriptword Exists(object value)
            => DbFunction("sqlExists", value);
    }
}

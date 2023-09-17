using BindOpen.System.Data;

namespace BindOpen.Labs.Databases.Data
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        // Comparison

        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static BdoExpression Eq(object value1, object value2)
            => DbFunction("sqlEq", value1, value2).AsExpression();

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static BdoExpression Diff(object value1, object value2)
            => DbFunction("sqlDiff", value1, value2).AsExpression();

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static BdoExpression Gt(object value1, object value2)
            => DbFunction("sqlGt", value1, value2).AsExpression();

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static BdoExpression Gte(object value1, object value2)
            => DbFunction("sqlGte", value1, value2).AsExpression();

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static BdoExpression Lt(object value1, object value2)
            => DbFunction("sqlLt", value1, value2).AsExpression();

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static BdoExpression Lte(object value1, object value2)
            => DbFunction("sqlLte", value1, value2).AsExpression();

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted object value.</returns>
        public static BdoExpression IsNull(object value1)
            => DbFunction("sqlIsNull", value1).AsExpression();

        /// <summary>
        /// Evaluates the script word $SQLIFNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted object value.</returns>
        public static BdoExpression IfNull(object value1, object value2)
            => DbFunction("sqlIfNull", value1, value2).AsExpression();

        /// <summary>
        /// Evaluates the script word $SQLEXISTS.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The interpreted object value.</returns>
        public static BdoExpression Exists(object value)
            => DbFunction("sqlExists", value).AsExpression();
    }
}

using BindOpen.Data.Expression;
using BindOpen.System.Scripting;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static DataExpression Text(object param1)
            => BdoScript.Function("sqlText", param1).CreateExp();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static DataExpression UpperCase(object param1)
            => DbFunction("sqlUCase", param1).CreateExp();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static DataExpression LowerCase(object param1)
            => DbFunction("sqlLCase", param1).CreateExp();

        /// <summary>
        /// Gets the Sql contenation of the specified object.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static DataExpression Concat(params object[] values)
            => DbFunction("sqlConcat", values).CreateExp();

        /// <summary>
        /// Gets the Sql string contenation of the specified object.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static DataExpression StringConcat(params object[] values)
            => DbFunction("sqlStringConcat", values).CreateExp();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter 1 to consider.</param>
        /// <param name="param2">The parameter 2 to consider.</param>
        /// <param name="param3">The parameter 3 to consider.</param>
        public static DataExpression LeftPadding(object param1, object param2, object param3)
            => DbFunction("sqlLPad", param1, param2, param3).CreateExp();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter 1 to consider.</param>
        /// <param name="param2">The parameter 2 to consider.</param>
        /// <param name="param3">The parameter 3 to consider.</param>
        public static DataExpression RightPadding(object param1, object param2, object param3)
            => DbFunction("sqlRPad", param1, param2, param3).CreateExp();
    }
}

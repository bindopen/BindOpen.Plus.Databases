using BindOpen.System.Scoping.Extensions.Scripting;
using BindOpen.System.Data.Items;

namespace BindOpen.Labs.Databases.Data
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
        public static BdoExpression Text(object param1)
            => BdoScript.Function("sqlText", param1).AsExpression();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static BdoExpression UpperCase(object param1)
            => DbFunction("sqlUCase", param1).AsExpression();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static BdoExpression LowerCase(object param1)
            => DbFunction("sqlLCase", param1).AsExpression();

        /// <summary>
        /// Gets the Sql contenation of the specified object.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static BdoExpression Concat(params object[] values)
            => DbFunction("sqlConcat", values).AsExpression();

        /// <summary>
        /// Gets the Sql string contenation of the specified object.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static BdoExpression StringConcat(params object[] values)
            => DbFunction("sqlStringConcat", values).AsExpression();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter 1 to consider.</param>
        /// <param name="param2">The parameter 2 to consider.</param>
        /// <param name="param3">The parameter 3 to consider.</param>
        public static BdoExpression LeftPadding(object param1, object param2, object param3)
            => DbFunction("sqlLPad", param1, param2, param3).AsExpression();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter 1 to consider.</param>
        /// <param name="param2">The parameter 2 to consider.</param>
        /// <param name="param3">The parameter 3 to consider.</param>
        public static BdoExpression RightPadding(object param1, object param2, object param3)
            => DbFunction("sqlRPad", param1, param2, param3).AsExpression();
    }
}

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
        /// Creates a BDO script representing the current date in SQL.
        /// </summary>
        public static DataExpression CurrentDate()
            => DbFunction("sqlGetCurrentDate").CreateExp();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static DataExpression Text(object param1)
            => BdoScript.Function("sqlText", param1).CreateExp();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        public static DataExpression Null()
            => DbFunction("sqlNull").CreateExp();

        /// <summary>
        /// Encodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static DataExpression EncodeBase64(object value)
            => DbFunction("sqlEncodeBase64", value).CreateExp();

        /// <summary>
        /// Decodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static DataExpression DecodeBase64(object value)
            => DbFunction("sqlDecodeBase64", value).CreateExp();

        /// <summary>
        /// Gets the Sql value of the specified object.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static DataExpression Value(object value)
            => BdoScript.Function("sqlValue", value).CreateExp();

        /// <summary>
        /// Gets the Sql contenation of the specified object.
        /// </summary>
        /// <param name="values">The values to consider.</param>
        public static DataExpression Concat(params object[] values)
            => DbFunction("sqlConcatenate", values).CreateExp();
    }
}

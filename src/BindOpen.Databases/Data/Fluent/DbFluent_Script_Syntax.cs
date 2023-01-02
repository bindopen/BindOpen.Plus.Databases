using BindOpen.Extensions.Scripting;
using BindOpen.Data.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a BDO script representing the current date in SQL.
        /// </summary>
        public static BdoExpression CurrentDate()
            => DbFunction("sqlGetCurrentDate").AsExpression();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        public static BdoExpression Null()
            => DbFunction("sqlNull").AsExpression();

        /// <summary>
        /// Encodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static BdoExpression EncodeBase64(object value)
            => DbFunction("sqlEncodeBase64", value).AsExpression();

        /// <summary>
        /// Decodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static BdoExpression DecodeBase64(object value)
            => DbFunction("sqlDecodeBase64", value).AsExpression();

        /// <summary>
        /// Gets the Sql value of the specified object.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static BdoExpression Value(object value)
            => BdoScript.Function("sqlValue", value).AsExpression();
    }
}

using BindOpen.Scoping.Script;

namespace BindOpen.Databases
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class BdoDb
    {
        /// <summary>
        /// Creates a BDO script representing the current date in SQL.
        /// </summary>
        public static IBdoScriptword CurrentDate()
            => DbFunction("sqlGetCurrentDate");

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        public static IBdoScriptword Null()
            => DbFunction("sqlNull");

        /// <summary>
        /// Encodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static IBdoScriptword EncodeBase64(object value)
            => DbFunction("sqlEncodeBase64", value);

        /// <summary>
        /// Decodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static IBdoScriptword DecodeBase64(object value)
            => DbFunction("sqlDecodeBase64", value);

        /// <summary>
        /// Gets the Sql value of the specified object.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static IBdoScriptword Value(object value)
            => BdoScript.Function("sqlValue", value);
    }
}

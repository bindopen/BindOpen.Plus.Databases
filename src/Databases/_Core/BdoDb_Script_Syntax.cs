using BindOpen.Kernel.Scoping.Script;

namespace BindOpen.Plus.Databases
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class BdoDb
    {
        /// <summary>
        /// Creates a BDO script representing the current date in SQL.
        /// </summary>
        public static BdoScriptword CurrentDate()
            => DbFunction("sqlGetCurrentDate");

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        public static BdoScriptword Null()
            => DbFunction("sqlNull");

        /// <summary>
        /// Encodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static BdoScriptword EncodeBase64(object value)
            => DbFunction("sqlEncodeBase64", value);

        /// <summary>
        /// Decodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static BdoScriptword DecodeBase64(object value)
            => DbFunction("sqlDecodeBase64", value);

        /// <summary>
        /// Gets the Sql value of the specified object.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static BdoScriptword Value(object value)
            => BdoScript.Function("sqlValue", value);
    }
}

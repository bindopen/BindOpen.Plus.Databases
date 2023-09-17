using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping.Script;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        // As parameter -----

        /// <summary>
        /// Creates a new instance of the BdoMetaData class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The data table to consider.</para>
        public static BdoMetaScalar Parameter(
            string name,
            object value = null)
        {
            return DbFluent.Parameter(name, DataValueTypes.Any, value);
        }

        /// <summary>
        /// Creates a new instance of the BdoMetaData class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The data value type to consider.</param>
        /// <param name="value">The data table to consider.</param>
        public static BdoMetaScalar Parameter(
            string name,
            DataValueTypes valueType,
            object value)
        {
            return BdoData.NewScalar(name, valueType, value);
        }

        /// <summary>
        /// Converts this instance as a word.
        /// </summary>
        /// <param name="element">The parameter to consider.</param>
        public static IBdoExpression AsExp(this IBdoMetaScalar parameter)
        {
            return (BdoExpression)BdoScript.Function("sqlParameter", parameter?.Name ?? parameter.Index.ToString());
        }

        /// <summary>
        /// Creates a parameter wild string from the specified parameter name.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        /// <returns>Returns the string corresponding to the specified parameter.</returns>
        internal static string AsParameterWildString(this string value)
            => StringHelper.__UniqueToken + "p:" + value + StringHelper.__UniqueToken;
    }
}

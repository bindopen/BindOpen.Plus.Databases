using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using BindOpen.System.Scripting;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        // As parameter -----

        /// <summary>
        /// Creates a new instance of the DataElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The data table to consider.</para>
        public static IDataElement Parameter(
            string name,
            object value = null)
        {
            return DbFluent.Parameter(name, DataValueTypes.Any, value);
        }

        /// <summary>
        /// Creates a new instance of the DataElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The data value type to consider.</param>
        /// <param name="value">The data table to consider.</param>
        public static IDataElement Parameter(
            string name,
            DataValueTypes valueType,
            object value)
        {
            return ElementFactory.CreateScalar(name, valueType, value);
        }

        /// <summary>
        /// Converts this instance as a word.
        /// </summary>
        /// <param name="element">The parameter to consider.</param>
        public static DataExpression AsExp(this ScalarElement parameter)
        {
            return BdoScript.Function("sqlParameter", parameter?.Name ?? parameter.Index.ToString())
                .CreateExp();
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

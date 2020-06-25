using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Carriers;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Scripting;
using System.Linq;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static BdoScriptword DbFunction(string name, params object[] parameters)
        {
            return BdoScript.Function(name, parameters.Select(p => p.AsSqlValue()).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static object AsSqlValue(this object obj)
        {
            if (obj == null)
            {
                return Null();
            }
            else if (obj is DbField field)
            {
                return (field?.ToScript()).CreateExpAsScript();
            }
            else if (obj is DbTable table)
            {
                return (table?.ToScript()).CreateExpAsScript();
            }
            else if (obj is ScalarElement param)
            {
                return BdoScript.Function("sqlParameter", param?.Name ?? param.Index.ToString())
                    .CreateExp();
            }
            else if (obj is DataExpression || obj is BdoScriptword)
            {
                return obj;
            }

            return obj?.ToString(DataValueTypes.Any, true);
        }

        /// <summary>
        /// Creates a BDO script representing the current date in SQL.
        /// </summary>
        public static DataExpression CurrentDate()
            => BdoScript.Function("sqlGetCurrentDate").CreateExp();

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
            => BdoScript.Function("sqlNull").CreateExp();

        /// <summary>
        /// Encodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static DataExpression EncodeBase64(object value)
            => BdoScript.Function("sqlEncodeBase64", value).CreateExp();

        /// <summary>
        /// Decodes the specified text with the specified format.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static DataExpression DecodeBase64(object value)
            => BdoScript.Function("sqlDecodeBase64", value).CreateExp();

        /// <summary>
        /// Gets the Sql value of the specified object.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        public static DataExpression Value(object value)
            => BdoScript.Function("sqlValue", value).CreateExp();
    }
}

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
    }
}

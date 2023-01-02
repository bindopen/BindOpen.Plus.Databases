using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Extensions.Scripting;
using System.Linq;

namespace BindOpen.Databases.Data
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
            else if (obj is string text)
            {
                return BdoScript.Function("sqlText", text);
            }
            else if (obj is DbField field)
            {
                return (field?.ToScript()).AsExpression(BdoExpressionKind.Script);
            }
            else if (obj is DbTable table)
            {
                return (table?.ToScript()).AsExpression(BdoExpressionKind.Script);
            }
            else if (obj is IScalarElement param)
            {
                return param.AsExp();
            }
            else if (obj is BdoExpression || obj is BdoScriptword)
            {
                return obj;
            }
            else if (obj.GetType().IsNumeric())
            {
                return obj;
            }

            return obj?.ToString(DataValueTypes.Any, true);
        }
    }
}

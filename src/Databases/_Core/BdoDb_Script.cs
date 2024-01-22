using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Databases.Models;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Databases
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class BdoDb
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal static IBdoScriptword DbFunction(string name, params object[] parameters)
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
            else if (obj is IDbField field)
            {
                return (field?.ToString()).ToExpression(BdoExpressionKind.Script);
            }
            else if (obj is IDbTable table)
            {
                return (table?.ToString()).ToExpression(BdoExpressionKind.Script);
            }
            //else if (obj is IBdoMetaScalar param)
            //{
            //    return param.AsExp();
            //}
            else if (obj is IBdoExpression)
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

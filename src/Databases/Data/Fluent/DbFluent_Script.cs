﻿using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping.Script;
using System.Linq;

namespace BindOpen.Plus.Databases.Data
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
                return (field?.ToScript()).ToExpression(BdoExpressionKind.Script);
            }
            else if (obj is DbTable table)
            {
                return (table?.ToScript()).ToExpression(BdoExpressionKind.Script);
            }
            else if (obj is IBdoMetaScalar param)
            {
                return param.AsExp();
            }
            else if (obj is BdoExpression)
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

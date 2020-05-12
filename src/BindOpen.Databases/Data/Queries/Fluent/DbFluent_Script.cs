using BindOpen.Data.Common;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using System;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data query parameter.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a BDO script representing the current date in SQL.
        /// </summary>
        public static DataExpression CurrentDate()
            => "$sqlGetCurrentDate()".CreateScript();

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static DataExpression Text(string param1)
        {
            return $"$sqlText('{(param1 == null ? Null() : (param1.Length == 0 ? "''" : param1))}')".CreateScript();
        }

        /// <summary>
        /// Creates a BDO script representing a text.
        /// </summary>
        public static DataExpression Null()
            => ("$sqlNull()").CreateScript();

        /// <summary>
        /// Encodes the specified text with the specified format.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public static DataExpression EncodeBase64(string text)
            => ("$sqlEncodeBase64(" + text + ")").CreateScript();

        /// <summary>
        /// Decodes the specified text with the specified format.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        public static DataExpression DecodeBase64(string text)
            => ("$sqlDecodeBase64(" + text + ")").CreateScript();

        /// <summary>
        /// Creates a BDO script representing a value.
        /// </summary>
        /// <param name="param1">The parameter to consider.</param>
        public static DataExpression Value(object param1)
        {
            if (param1 == null)
            {
                return DbFluent.Null();
            }

            if (param1 is DataExpression param1DataExpression)
            {
                if (param1DataExpression.Kind == DataExpressionKind.Auto
                    && param1DataExpression.Text?.StartsWith("{{") == true
                    && param1DataExpression.Text?.EndsWith("}}") == true)
                {

                    var text = param1DataExpression.Text.Substring(2);
                    return (text[0..^2]).CreateScript();
                }
                return param1DataExpression;
            }

            var valueType = param1.GetValueType();
            switch (valueType)
            {
                case DataValueTypes.Text:
                    var param1String = param1 as string;
                    return Text(param1String);
                case DataValueTypes.Date:
                    if (param1 is DateTime param1DateTime)
                    {
                        return Text(param1DateTime.ToString(StringHelper.__DateFormat));
                    }
                    break;
                case DataValueTypes.Time:
                    if (param1 is TimeSpan param1TimeSpan)
                    {
                        return Text(param1TimeSpan.ToString(StringHelper.__TimeFormat));
                    }
                    break;
            }

            return (param1?.ToString()).CreateScript();
        }
    }
}

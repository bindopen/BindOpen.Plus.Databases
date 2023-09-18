using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// This class represents the database data query extension.
    /// </summary>
    public static partial class DbQueryExtension
    {
        private static string GetScriptFunction(DataOperators op)
        {
            switch (op)
            {
                case DataOperators.Contains:
                    return "$sqlLike";
                case DataOperators.DifferentFrom:
                    return "$sqlDiff";
                case DataOperators.EqualTo:
                    return "$sqlEq";
                case DataOperators.GreaterThan:
                    return "$sqlGt";
                case DataOperators.GreaterOrEqualThan:
                    return "$sqlGte";
                case DataOperators.Has:
                    return "";
                case DataOperators.In:
                    return "$sqlIn";
                case DataOperators.LesserThan:
                    return "$sqlLt";
                case DataOperators.LesserOrEqualThan:
                    return "$sqlLte";
            }

            return null;
        }

        /// <summary>
        /// Converts the specifed search query into an extension script.
        /// </summary>
        /// <param name="searchQuery">The search query to consider.</param>
        /// <param name="log">The </param>
        /// <param name="definition">The clause statement to consider.</param>
        /// <param name="i"></param>
        /// <returns></returns>
        internal static string ConvertToExtensionScript(
            this string searchQuery,
            IBdoLog log = null,
            IDbApiFilterDefinition definition = null,
            int i = 0)
        {
            string script = searchQuery;

            if (!string.IsNullOrEmpty(script))
            {
                // boolean instructions

                foreach (string instruction in new string[] { "Or", "And", "Not" })
                {
                    int j = i;
                    List<string> clauses = new();
                    script.IndexOfNextString(" " + instruction + " ", ref j);
                    while (j < script.Length - 1)
                    {
                        string clause = script.Substring(i, j - i + 1);
                        clause = clause.ConvertToExtensionScript(log, definition, 0);
                        clauses.Add(clause);
                        j = i = j + (" " + instruction + " ").Length;
                        script.IndexOfNextString(" " + instruction + " ", ref j);
                        if (j == script.Length)
                        {
                            clause = script[i..];
                            clause = clause.ConvertToExtensionScript(log, definition, 0);
                            clauses.Add(clause);
                        }
                    }
                    if (clauses.Count > 0)
                    {
                        script = "$sql" + instruction + "(" + clauses.Aggregate((p, q) => p + "," + q) + ")";
                    }
                }

                if (i == 0)
                {
                    DataOperators aOperator = DataOperators.None;

                    int k = script.Length;
                    string scriptOperator = null;
                    foreach (DataOperators currentOperator in new DataOperators[] {
                        DataOperators.Exists,
                        DataOperators.Contains,
                        DataOperators.DifferentFrom, DataOperators.EqualTo,
                        DataOperators.GreaterOrEqualThan, DataOperators.GreaterThan,
                        DataOperators.LesserOrEqualThan, DataOperators.LesserThan,
                        DataOperators.Has, DataOperators.In })
                    {
                        int k1 = 0;
                        string currentScriptOperator = DbQueryExtension.GetInstruction(currentOperator);
                        script.IndexOfNextString(currentScriptOperator, ref k1);
                        if (k1 < k)
                        {
                            scriptOperator = currentScriptOperator;
                            aOperator = currentOperator;
                            k = k1;
                        }
                    }
                    if (k == script.Length)
                    {
                        log?.AddEvent(EventKinds.Error, "No operator found in clause '" + searchQuery + "'", resultCode: "user");
                    }
                    else
                    {
                        string scriptFunction = DbQueryExtension.GetScriptFunction(aOperator)?.Trim();
                        string fieldName = script.Substring(0, k)?.Trim();
                        string value = script.Substring(k + scriptOperator.Length)?.Trim();

                        if (value.Length > 2 && value.StartsWith("'") && value.EndsWith("'"))
                            value = "$sqlText('" + value[1..^1] + "')";

                        // check that the field is in the dictionary
                        if (!definition.ContainsKey(fieldName))
                        {
                            log?.AddEvent(EventKinds.Error, "Undefined field '" + fieldName + "' in clause '" + searchQuery + "''", resultCode: "user");
                        }
                        else
                        {
                            IDbApiFilterClause clause = definition?[fieldName];

                            // check the instruction found corresponds to the definition in dictionary
                            if (!clause.Operators.Any(p => p == aOperator))
                            {
                                log?.AddEvent(EventKinds.Error, "Undefined operator '" + aOperator.ToString() + "' for field '" + fieldName + "'", resultCode: "user");
                            }
                            else
                            {
                                clause.Field ??= BdoDb.Field(fieldName);

                                if (aOperator == DataOperators.Has)
                                {
                                    if (value.Length > 2 && value.StartsWith("{") && value.EndsWith("}"))
                                        value = value[1..^1];
                                    value = value.ConvertToExtensionScript(log, clause.FilterDefinition, 0);
                                    script = "(" + value + ")";
                                }
                                else
                                {
                                    script = scriptFunction + clause.Field;
                                }
                            }
                        }
                    }
                }
            }
            return script;
        }

        private static string GetInstruction(DataOperators op)
        {
            switch (op)
            {
                case DataOperators.Contains:
                    return "constains";
                case DataOperators.DifferentFrom:
                    return "!=";
                case DataOperators.EqualTo:
                    return "=";
                case DataOperators.GreaterThan:
                    return ">";
                case DataOperators.GreaterOrEqualThan:
                    return ">=";
                case DataOperators.Has:
                    return "has";
                case DataOperators.In:
                    return "in";
                case DataOperators.LesserThan:
                    return "<";
                case DataOperators.LesserOrEqualThan:
                    return "<=";
            }

            return null;
        }
    }
}

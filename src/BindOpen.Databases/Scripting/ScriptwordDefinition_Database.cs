using BindOpen.Databases.Builders;
using BindOpen.Databases.Data;
using BindOpen.Framework.Extensions.Scripting;
using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Stores;

namespace BindOpen.Databases.Scripting
{
    /// <summary>
    /// This class represents a 'Database' script word definition.
    /// </summary>
    [BdoScriptwordDefinition()]
    public static class ScriptwordDefinition_Database
    {
        // ------------------------------------------
        // FUNCTIONS
        // ------------------------------------------

        #region Functions

        // Aggregate

        /// <summary>
        /// Evaluates the script word $SQLCOUNT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlCount")]
        public static object Fun_SqlCount(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Count(domain?.Scriptword?.Parameters?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLSUM.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlSum")]
        public static object Fun_SqlSum(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Sum(domain?.Scriptword?.Parameters?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLAVG.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlAverage")]
        public static object Fun_SqlAverage(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Average(domain?.Scriptword?.Parameters?.ToArray());
            }
        }

        // Date and time

        /// <summary>
        /// Evaluates the script word $SQLGETCURRENTDATE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlGetCurrentDate")]
        public static object Fun_SqlGetCurrentDate(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_CurrentDate();
            }
        }

        // Date type

        // Logical

        /// <summary>
        /// Evaluates the script word $SQLTRUE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlTrue")]
        public static object Fun_SqlTrue(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Value(true);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLIF.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlIf")]
        public static object Fun_SqlIf(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string condition = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value1 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(2)?.ToString();

                return queryBuilder.GetSqlText_If(condition, value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLNOT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlNot")]
        public static object Fun_SqlNot(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                return queryBuilder.GetSqlText_Not(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLOR.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlOr")]
        public static object Fun_SqlOr(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Or(domain?.Scriptword?.Parameters?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLAND.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlAnd")]
        public static object Fun_SqlAnd(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_And(domain?.Scriptword?.Parameters?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLXOR.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlXor")]
        public static object Fun_SqlXor(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Xor(domain?.Scriptword?.Parameters?.ToArray());
            }
        }

        // Comparison
        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlEq")]
        public static object Fun_SqlEq(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();

                return queryBuilder.GetSqlText_Eq(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlDiff")]
        public static object Fun_SqlDiff(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();

                return queryBuilder.GetSqlText_Diff(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlGt")]
        public static object Fun_SqlGt(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();

                return queryBuilder.GetSqlText_Gt(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlGte")]
        public static object Fun_SqlGte(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();

                return queryBuilder.GetSqlText_Gte(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlLt")]
        public static object Fun_SqlLt(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();

                return queryBuilder.GetSqlText_Lt(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlLte")]
        public static object Fun_SqlLte(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();

                return queryBuilder.GetSqlText_Lte(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlIsNull")]
        public static object Fun_SqlIsNull(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();

                return queryBuilder.GetSqlText_IsNull(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLIFNULL.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlIfNull")]
        public static object Fun_SqlIfNull(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();

                return queryBuilder.GetSqlText_IfNull(value1, value2);
            }
        }

        // Conversion

        /// <summary>
        /// Evaluates the script word $SQLCONVERTTOTEXT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlConvertToText")]
        public static object Fun_SqlConvertToText(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                return queryBuilder.GetSqlText_ConvertToText(value1);
            }
        }

        // String

        /// <summary>
        /// Evaluates the script word $SQLTEXT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlText")]
        public static object Fun_SqlText(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                return queryBuilder.GetSqlText_Text(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLVALUE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlValue")]
        public static object Fun_SqlValue(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                return queryBuilder.GetSqlText_Value(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLENCODEBASE64.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlEncodeBase64")]
        public static object Fun_SqlEncodeBase64(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                return queryBuilder.GetSqlText_EncodeBase64(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLDECODEBASE64.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlDecodeBase64")]
        public static object Fun_SqlDecodeBase64(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                return queryBuilder.GetSqlText_DecodeBase64(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLIKE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlLike")]
        public static object Fun_SqlLike(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();
                return queryBuilder.GetSqlText_Like(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLREPLACE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlReplace")]
        public static object Fun_SqlReplace(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();
                string value3 = domain?.Scriptword?.Parameters?.GetAt(2)?.ToString();
                return queryBuilder.GetSqlText_Replace(value1, value2, value3);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLCONCAT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlConcat")]
        public static object Fun_SqlConcat(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Concat(domain?.Scriptword?.Parameters?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLSTRINGCONCAT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlStringConcat")]
        public static object Fun_SqlStringConcat(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_StringConcat(domain?.Scriptword?.Parameters?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLNULL.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlNull")]
        public static object Fun_SqlNull(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Null();
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLEMPTY.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlEmpty")]
        public static object Fun_SqlEmpty(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Empty();
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLCASE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlLCase")]
        public static object Fun_SqlLower(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                return queryBuilder.GetSqlText_LCase(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLUCASE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlUCase")]
        public static object Fun_SqlUpper(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                return queryBuilder.GetSqlText_UCase(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLPAD.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlLPad")]
        public static object Fun_SqlLeftPadding(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();
                string value3 = domain?.Scriptword?.Parameters?.GetAt(2)?.ToString();
                return queryBuilder.GetSqlText_LPad(value1, value2, value3);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLRPAD.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlRPad")]
        public static object Fun_SqlRightPadding(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();
                string value3 = domain?.Scriptword?.Parameters?.GetAt(2)?.ToString();
                return queryBuilder.GetSqlText_RPad(value1, value2, value3);
            }
        }

        // Syntax

        /// <summary>
        /// Evaluates the script word %SQLDATABASE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlDatabase")]
        public static object Fun_SqlDatabase(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();

                string instanceName = domain?.Scope?.DataStore?.Get<IBdoDatasourceDepot>()?.GetInstanceName(value1);
                if (string.IsNullOrEmpty(instanceName) || instanceName == StringHelper.__NoneString)
                    instanceName = value1;

                return queryBuilder.GetSqlText_Database(instanceName);
            }
        }

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%SCHEMA.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_SqlDatabase_SqlSchema(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parent?.Item?.ToString();
                return queryBuilder.GetSqlText_Schema(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%TABLE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_SqlDatabase_SqlTable(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parent?.Item?.ToString();
                return queryBuilder.GetSqlText_Table(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word [[%DATABASE->]%TABLE->]%FIELD.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_SqlDatabase_SqlTable_SqlField(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parent?.Item?.ToString();
                return queryBuilder.GetSqlText_Field(value1, value2);
            }
        }

        // System

        /// <summary>
        /// Evaluates the script word $SQLNEWGUID.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlNewGuid")]
        public static object Fun_SqlNewGuid(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_NewGuid();
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLRANDOM.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlRandom")]
        public static object Fun_SqlRandom(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Random();
            }
        }

        // Comparison

        /// <summary>
        /// Evaluates the script word $SQLIN.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlIn")]
        public static object Fun_SqlIn(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                string value2 = domain?.Scriptword?.Parameters?.GetAt(1)?.ToString();

                return queryBuilder.GetSqlText_In(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLEXISTS.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlExists")]
        public static object Fun_SqlExists(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
                return queryBuilder.GetSqlText_Exists(value);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLIST.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlList")]
        public static object Fun_SqlList(IBdoScriptwordDomain domain)
        {
            var queryBuilder = domain?.ScriptVariableSet?.GetDbBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_List(domain?.Scriptword?.Parameters?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLPARAMETER.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlParameter")]
        public static object Fun_SqlParameter(IBdoScriptwordDomain domain)
        {
            string value = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
            return value.AsParameterWildString();
        }

        /// <summary>
        /// Evaluates the script word $SQLQUERY.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(Name = "sqlQuery")]
        public static object Fun_SqlQuery(IBdoScriptwordDomain domain)
        {
            string value = domain?.Scriptword?.Parameters?.GetAt(0)?.ToString();
            return "(" + value.AsQueryWildString() + ")";
        }

        #endregion
    }
}
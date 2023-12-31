using BindOpen.Scoping;
using BindOpen.Scoping.Script;

namespace BindOpen.Plus.Databases.Script
{
    /// <summary>
    /// This class represents a 'Database' script word definition.
    /// </summary>
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
        [BdoFunction(Name = "sqlCount")]
        public static object Fun_SqlCount(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Count(domain?.Scriptword?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLSUM.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlSum")]
        public static object Fun_SqlSum(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Sum(domain?.Scriptword?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLAVG.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlAverage")]
        public static object Fun_SqlAverage(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Average(domain?.Scriptword?.ToArray());
            }
        }

        // Date and time

        /// <summary>
        /// Evaluates the script word $SQLGETCURRENTDATE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlGetCurrentDate")]
        public static object Fun_SqlGetCurrentDate(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
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
        [BdoFunction(Name = "sqlTrue")]
        public static object Fun_SqlTrue(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
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
        [BdoFunction(Name = "sqlIf")]
        public static object Fun_SqlIf(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string condition = domain?.Scriptword?.GetData<string>(0);
                string value1 = domain?.Scriptword?.GetData<string>(1);
                string value2 = domain?.Scriptword?.GetData<string>(2);

                return queryBuilder.GetSqlText_If(condition, value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLNOT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlNot")]
        public static object Fun_SqlNot(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                return queryBuilder.GetSqlText_Not(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLOR.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlOr")]
        public static object Fun_SqlOr(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Or(domain?.Scriptword?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLAND.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlAnd")]
        public static object Fun_SqlAnd(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_And(domain?.Scriptword?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLXOR.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlXor")]
        public static object Fun_SqlXor(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Xor(domain?.Scriptword?.ToArray());
            }
        }

        // Comparison
        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlEq")]
        public static object Fun_SqlEq(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);

                return queryBuilder.GetSqlText_Eq(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlDiff")]
        public static object Fun_SqlDiff(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);

                return queryBuilder.GetSqlText_Diff(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlGt")]
        public static object Fun_SqlGt(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);

                return queryBuilder.GetSqlText_Gt(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlGte")]
        public static object Fun_SqlGte(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);

                return queryBuilder.GetSqlText_Gte(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlLt")]
        public static object Fun_SqlLt(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);

                return queryBuilder.GetSqlText_Lt(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlLte")]
        public static object Fun_SqlLte(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);

                return queryBuilder.GetSqlText_Lte(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlIsNull")]
        public static object Fun_SqlIsNull(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);

                return queryBuilder.GetSqlText_IsNull(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLIFNULL.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlIfNull")]
        public static object Fun_SqlIfNull(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);

                return queryBuilder.GetSqlText_IfNull(value1, value2);
            }
        }

        // Conversion

        /// <summary>
        /// Evaluates the script word $SQLCONVERTTOTEXT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlConvertToText")]
        public static object Fun_SqlConvertToText(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                return queryBuilder.GetSqlText_ConvertToText(value1);
            }
        }

        // String

        /// <summary>
        /// Evaluates the script word $SQLTEXT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlText")]
        public static object Fun_SqlText(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                return queryBuilder.GetSqlText_Text(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLVALUE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlValue")]
        public static object Fun_SqlValue(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                return queryBuilder.GetSqlText_Value(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLENCODEBASE64.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlEncodeBase64")]
        public static object Fun_SqlEncodeBase64(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                return queryBuilder.GetSqlText_EncodeBase64(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLDECODEBASE64.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlDecodeBase64")]
        public static object Fun_SqlDecodeBase64(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                return queryBuilder.GetSqlText_DecodeBase64(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLIKE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlLike")]
        public static object Fun_SqlLike(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);
                return queryBuilder.GetSqlText_Like(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLREPLACE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlReplace")]
        public static object Fun_SqlReplace(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);
                string value3 = domain?.Scriptword?.GetData<string>(2);
                return queryBuilder.GetSqlText_Replace(value1, value2, value3);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLCONCAT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlConcat")]
        public static object Fun_SqlConcat(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_Concat(domain?.Scriptword?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLSTRINGCONCAT.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlStringConcat")]
        public static object Fun_SqlStringConcat(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_StringConcat(domain?.Scriptword?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLNULL.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlNull")]
        public static object Fun_SqlNull(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
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
        [BdoFunction(Name = "sqlEmpty")]
        public static object Fun_SqlEmpty(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
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
        [BdoFunction(Name = "sqlLCase")]
        public static object Fun_SqlLower(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                return queryBuilder.GetSqlText_LCase(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLUCASE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlUCase")]
        public static object Fun_SqlUpper(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                return queryBuilder.GetSqlText_UCase(value1);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLPAD.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlLPad")]
        public static object Fun_SqlLeftPadding(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);
                string value3 = domain?.Scriptword?.GetData<string>(2);
                return queryBuilder.GetSqlText_LPad(value1, value2, value3);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLRPAD.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlRPad")]
        public static object Fun_SqlRightPadding(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.GetData<string>(1);
                string value3 = domain?.Scriptword?.GetData<string>(2);
                return queryBuilder.GetSqlText_RPad(value1, value2, value3);
            }
        }

        // Syntax

        /// <summary>
        /// Evaluates the script word %SQLDATABASE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlDatabase")]
        public static object Fun_SqlDatabase(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                return value1;
            }
        }

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%SCHEMA.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction]
        public static object Fun_SqlDatabase_SqlSchema(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.Parent?.GetData<string>();
                return queryBuilder.GetSqlText_Schema(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%TABLE.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction]
        public static object Fun_SqlDatabase_SqlTable(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.Parent?.GetData<string>();
                return queryBuilder.GetSqlText_Table(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word [[%DATABASE->]%TABLE->]%FIELD.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction]
        public static object Fun_SqlDatabase_SqlTable_SqlField(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?.Parent?.GetData<string>();
                return queryBuilder.GetSqlText_Field(value1, value2);
            }
        }

        // System

        /// <summary>
        /// Evaluates the script word $SQLNEWGUID.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlNewGuid")]
        public static object Fun_SqlNewGuid(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
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
        [BdoFunction(Name = "sqlRandom")]
        public static object Fun_SqlRandom(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
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
        [BdoFunction(Name = "sqlIn")]
        public static object Fun_SqlIn(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value1 = domain?.Scriptword?.GetData<string>(0);
                string value2 = domain?.Scriptword?[1]?.ToString();

                return queryBuilder.GetSqlText_In(value1, value2);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLEXISTS.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlExists")]
        public static object Fun_SqlExists(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                string value = domain?.Scriptword?.GetData<string>(0);
                return queryBuilder.GetSqlText_Exists(value);
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLLIST.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlList")]
        public static object Fun_SqlList(IBdoScriptDomain domain)
        {
            var queryBuilder = domain?.VariableSet?.GetDbQueryBuilder();
            if (queryBuilder == null)
            {
                return "<DatabaseBuilderMissing/>";
            }
            else
            {
                return queryBuilder.GetSqlText_List(domain?.Scriptword?.ToArray());
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLPARAMETER.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlParameter")]
        public static object Fun_SqlParameter(IBdoScriptDomain domain)
        {
            string value = domain?.Scriptword?.GetData<string>(0);
            return value.AsParameterWildString();
        }

        /// <summary>
        /// Evaluates the script word $SQLQUERY.
        /// </summary>
        /// <param name="scope">The script word function scope to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "sqlQuery")]
        public static object Fun_SqlQuery(IBdoScriptDomain domain)
        {
            string value = domain?.Scriptword?.GetData<string>(0);
            return "(" + value.AsQueryWildString() + ")";
        }

        #endregion
    }
}
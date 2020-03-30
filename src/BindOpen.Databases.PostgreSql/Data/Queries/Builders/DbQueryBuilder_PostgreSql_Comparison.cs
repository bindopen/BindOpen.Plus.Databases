using System;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_PostgreSql
    {
        // Comparison

        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Eq(string value1, string value2)
        {
            if ((value1 == null || string.Equals(value1, GetSqlText_Null(), StringComparison.OrdinalIgnoreCase))
                && value2 != null)
            {
                return value2 + " is null";
            }
            else if ((value2 == null || string.Equals(value2, GetSqlText_Null(), StringComparison.OrdinalIgnoreCase))
                && value1 != null)
            {
                return value1 + " is null";
            }

            return value1 + "=" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Diff(string value1, string value2)
        {
            return value1 + "<>" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Gt(string value1, string value2)
        {
            return value1 + ">" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Gte(string value1, string value2)
        {
            return value1 + ">=" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Lt(string value1, string value2)
        {
            return value1 + "<" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Lte(string value1, string value2)
        {
            return value1 + "<=" + value2;
        }

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_IsNull(string value1)
        {
            return value1 + " IS NULL";
        }
    }
}
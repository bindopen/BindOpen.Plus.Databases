namespace BindOpen.Databases.Relational.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_PostgreSql
    {
        // Conversion

        /// <summary>
        /// Evaluates the script word $SQLCONVERTTOTEXT.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_ConvertToText(string value1)
        {
            return "convert(varchar, " + value1 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLISNULLREPLACE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_IfNull(string value1, string value2)
        {
            return "coalesce(" + value1 + ", " + value2 + ")";
        }
    }
}
namespace BindOpen.Databases.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial interface IDbQueryBuilder
    {
        // Conversion

        /// <summary>
        /// Evaluates the script word $SQLCONVERTTOTEXT.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_ConvertToText(string value1);

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_IfNull(string value1, string value2);

    }
}
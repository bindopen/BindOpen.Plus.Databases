namespace BindOpen.Databases.Relational.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial interface IDbQueryBuilder
    {
        // Comparison

        /// <summary>
        /// Evaluates the script word $SQLEQ.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Eq(string value1, string value2);

        /// <summary>
        /// Evaluates the script word $SQLDIFF.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Diff(string value1, string value2);

        /// <summary>
        /// Evaluates the script word $SQLGT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Gt(string value1, string value2);

        /// <summary>
        /// Evaluates the script word $SQLGTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Gte(string value1, string value2);

        /// <summary>
        /// Evaluates the script word $SQLLT.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Lt(string value1, string value2);

        /// <summary>
        /// Evaluates the script word $SQLLTE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Lte(string value1, string value2);

        /// <summary>
        /// Evaluates the script word $SQLISNULL.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_IsNull(string value1);

        /// <summary>
        /// Evaluates the script word $SQLIN.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_In(params object[] parameters);

        /// <summary>
        /// Evaluates the script word $SQLEXISTS.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Exists(string value);
    }
}
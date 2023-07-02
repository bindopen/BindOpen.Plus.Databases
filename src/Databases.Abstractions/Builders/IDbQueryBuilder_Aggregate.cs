namespace BindOpen.Labs.Databases.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial interface IDbQueryBuilder
    {
        // Aggregate

        /// <summary>
        /// Evaluates the script word $SQLCOUNT.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetSqlText_Count(params object[] parameters);

        /// <summary>
        /// Evaluates the script word $SQLSUM.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Sum(params object[] parameters);

        /// <summary>
        /// Evaluates the script word $SQLAVG.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Average(params object[] parameters);
    }
}
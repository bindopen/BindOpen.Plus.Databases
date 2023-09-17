namespace BindOpen.Labs.Databases.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial interface IDbQueryBuilder
    {
        // Syntax

        /// <summary>
        /// Evaluates the script word $SQLNULL.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Null();

        /// <summary>
        /// Evaluates the script word $SQLVALUE.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Value(object value);

        /// <summary>
        /// Evaluates the script word %SQLDATABASE.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Database(string name);

        /// <summary>
        /// Evaluates the script word %SQLSCHEMA.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Schema(
            string name,
            string location = null);

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%TABLE.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Table(
            string name,
            string location = null);

        /// <summary>
        /// Evaluates the script word [[%DATABASE->]%TABLE->]%FIELD.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Field(string name, string location);

        /// <summary>
        /// Evaluates the script word $SQLLIST.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_List(params object[] parameters);
    }
}
namespace BindOpen.Databases.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial interface IDbQueryBuilder
    {
        // String

        /// <summary>
        /// Evaluates the script word $SQLTEXT.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Text(string value1);

        /// <summary>
        /// Evaluates the script word $SQLLIKE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Like(string value1, string value2);

        /// <summary>
        /// Evaluates the script word $SQLREPLACE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Replace(string value1, string value2, string value3);

        /// <summary>
        /// Evaluates the script word $SQLCONCAT.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Concat(object[] parameters);

        /// <summary>
        /// Evaluates the script word $SQLSTRINGCONCATENATE.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_StringConcat(object[] parameters);

        /// <summary>
        /// Evaluates the script word $SQLEMPTY.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_Empty();

        /// <summary>
        /// Evaluates the script word $SQLLCASE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_LCase(string text);

        /// <summary>
        /// Evaluates the script word $SQLUCASE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_UCase(string text);

        /// <summary>
        /// Evaluates the script word $SQLLCASE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <param name="charCount">The number of characters to consider.</param>
        /// <param name="replaceText">The replacing text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_LPad(string text, string charCount, string replaceText);

        /// <summary>
        /// Evaluates the script word $SQLRCASE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <param name="charCount">The number of characters to consider.</param>
        /// <param name="replaceText">The replacing text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_RPad(string text, string charCount, string replaceText);

        /// <summary>
        /// Evaluates the script word $SQLDECODE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_DecodeBase64(string text);

        /// <summary>
        /// Evaluates the script word $SQLENCODE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        string GetSqlText_EncodeBase64(string text);
    }
}
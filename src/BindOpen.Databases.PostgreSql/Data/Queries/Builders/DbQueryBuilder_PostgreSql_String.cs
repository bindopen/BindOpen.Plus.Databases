namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
    {
        // String

        /// <summary>
        /// Evaluates the script word $SQLTEXT.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Text(string value1)
        {
            return "'" + value1?.Replace("'", "''") + "'";
        }

        /// <summary>
        /// Evaluates the script word $SQLLIKE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Like(string value1, string value2)
        {
            return "(" + value1 + " like " + value2 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLREPLACE.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Replace(string value1, string value2, string value3)
        {
            return "replace(" + value1 + ", " + value2 + ", " + value3 + ")";
        }

        /// <summary>
        /// Evaluates the script word $SQLCONCAT.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Concat(object[] parameters)
        {
            string text = "concat(";
            if (parameters.Length == 1)
            {
                text += parameters[0];
            }
            else
            {
                text += string.Join(", ", parameters);
            }

            text += ")";

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLSTRINGCONCATENATE.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_StringConcat(object[] parameters)
        {
            if (parameters.Length == 1)
            {
                return parameters[0]?.ToString();
            }

            return string.Join(" || ", parameters);
        }

        /// <summary>
        /// Evaluates the script word $SQLDECODE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_DecodeBase64(string text)
        {
            return "decode(" + text + ", 'base64')";
        }

        /// <summary>
        /// Evaluates the script word $SQLENCODE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_EncodeBase64(string text)
        {
            return "encode(" + text + ", 'base64')";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetSqlText_Empty()
        {
            return "''";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string GetSqlText_LCase(string text)
        {
            return "lower(" + text + ")";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string GetSqlText_UCase(string text)
        {
            return "upper(" + text + ")";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="charCount"></param>
        /// <param name="replaceText"></param>
        /// <returns></returns>
        public override string GetSqlText_LPad(string text, string charCount, string replaceText)
        {
            return $"lpad({text}, {charCount}, {replaceText})";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="charCount"></param>
        /// <param name="replaceText"></param>
        /// <returns></returns>
        public override string GetSqlText_RPad(string text, string charCount, string replaceText)
        {
            return $"rpad({text}, {charCount}, {replaceText})";
        }
    }
}
using BindOpen.System.Scripting;

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
            return "'" + BdoScriptParsingHelper.GetValueFromText(value1)?.Replace("'", "''") + "'";
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
        /// Evaluates the script word $SQLCONCATENATE.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Concatenate(object[] parameters)
        {
            string text = "";
            foreach (object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += (string.IsNullOrEmpty(text) ? st : " + " + st);
                }
            }

            return text;
        }

        /// <summary>
        /// Evaluates the script word $SQLDECODE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_DecodeBae64(string text)
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
    }
}
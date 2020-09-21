using BindOpen.System.Scripting;
using System;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // String

        /// <summary>
        /// Evaluates the script word $SQLTEXT.
        /// </summary>
        /// <param name="value1"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Text(string value1)
        {
            return "'" + value1.GetValueFromScript()?.Replace("'", "''") + "'";
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
            string text = "";
            foreach (object object1 in parameters)
            {
                if (object1 != null)
                {
                    string st = object1.ToString();
                    text += (text == String.Empty ? st : " + " + st);
                }
            }

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
            return "decode('" + text + "', 'base64')";
        }

        /// <summary>
        /// Evaluates the script word $SQLENCODE.
        /// </summary>
        /// <param name="text">The text to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_EncodeBase64(string text)
        {
            return "encode('" + text + "', 'base64')";
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
            return "lcase(" + text + ")";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string GetSqlText_UCase(string text)
        {
            return "ucase(" + text + ")";
        }
    }
}
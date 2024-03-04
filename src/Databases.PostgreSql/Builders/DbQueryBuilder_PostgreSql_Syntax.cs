using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Databases.Relational;
using System;

namespace BindOpen.Databases.Relational.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
    {
        // Syntax

        /// <summary>
        /// Evaluates the script word $SQLNULL.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Null()
        {
            return "null";
        }

        /// <summary>
        /// Evaluates the script word $SQLVALUE.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Value(object value)
        {
            if (value == null)
            {
                return BdoDb.Null().ToString();
            }
            else if (value is IDbQuery)
            {

            }

            var valueType = value.GetValueType();
            switch (valueType)
            {
                case DataValueTypes.Text:
                    var param1String = value as string;
                    return GetSqlText_Text(param1String);
                case DataValueTypes.Date:
                    if (value is DateTime param1DateTime)
                    {
                        return GetSqlText_Text(param1DateTime.ToString(StringHelper.__DateTimeFormat));
                    }
                    break;
                case DataValueTypes.Time:
                    if (value is TimeSpan param1TimeSpan)
                    {
                        return GetSqlText_Text(param1TimeSpan.ToString(StringHelper.__TimeFormat));
                    }
                    break;
            }
            return value?.ToString() ?? "";
        }

        /// <summary>
        /// Evaluates the script word %SQLDATABASE.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Database(string name)
        {
            return "\"" + name?.Replace("\"", "'\"\"") + "\"";
        }

        /// <summary>
        /// Evaluates the script word %SQLSCHEMA.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Schema(
            string name,
            string location = null)
        {
            return (!string.IsNullOrEmpty(location) ? location + "." : "") + "\"" + name?.Replace("\"", "'\"\"") + "\"";
        }

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%TABLE.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Table(
            string name,
            string location = null)
        {
            return (!string.IsNullOrEmpty(location) ? location + "." : "") + "\"" + name?.Replace("\"", "'\"\"") + "\"";
        }

        /// <summary>
        /// Evaluates the script word [[%DATABASE->]%TABLE->]%FIELD.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_Field(
            string name,
            string location = null)
        {
            return (!string.IsNullOrEmpty(location) ? location + "." : "") + "\"" + name?.Replace("\"", "'\"\"") + "\"";
        }

        /// <summary>
        /// Evaluates the script word $SQLLIST.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_List(params object[] parameters)
        {
            return "";
        }
    }
}
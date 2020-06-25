using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder
    {
        // Syntax

        /// <summary>
        /// Gets the Sql string corresponding to the specified value.
        /// </summary>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        /// <returns>Returns the Sql string.</returns>
        protected virtual string GetSqlText_Value(object value, DataValueTypes valueType = DataValueTypes.Text)
        {
            if (value == null)
            {
                return GetSqlText_Null();
            }

            switch (valueType)
            {
                case DataValueTypes.Text:
                    return value == null ? GetSqlText_Null() : GetSqlText_Text(value as string);
                case DataValueTypes.ByteArray:
                case DataValueTypes.Date:
                    return value == null ? GetSqlText_Null() : GetSqlText_Text(value.ToString(valueType));
                case DataValueTypes.Number:
                case DataValueTypes.Integer:
                case DataValueTypes.Long:
                case DataValueTypes.ULong:
                    return value == null ? GetSqlText_Null() : value.ToString(valueType);
                default:
                    return value == null ? GetSqlText_Null() : value.ToString();
            }
        }

        /// <summary>
        /// Evaluates the script word $SQLNULL.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public abstract string GetSqlText_Null();

        /// <summary>
        /// Evaluates the script word $SQLTRUE.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public abstract string GetSqlText_Value(object value);

        /// <summary>
        /// Evaluates the script word %SQLDATABASE.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interpreted string value.</returns>
        public abstract string GetSqlText_Database(string name);

        /// <summary>
        /// Evaluates the script word %SQLSCHEMA.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public abstract string GetSqlText_Schema(
            string name,
            string location = null);

        /// <summary>
        /// Evaluates the script word [%DATABASE->]%TABLE.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public abstract string GetSqlText_Table(
            string name,
            string location = null);

        /// <summary>
        /// Evaluates the script word [[%DATABASE->]%TABLE->]%FIELD.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns>The interpreted string value.</returns>
        public abstract string GetSqlText_Field(string name, string location);

        /// <summary>
        /// Evaluates the script word $SQLLIST.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public abstract string GetSqlText_List(params object[] parameters);
    }
}
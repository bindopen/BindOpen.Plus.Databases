using BindOpen.Data.Common;

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
        protected virtual string GetSqlText_Value(string value, DataValueType valueType = DataValueType.Text)
        {
            switch (valueType)
            {
                case DataValueType.Number:
                case DataValueType.Integer:
                case DataValueType.None:
                case DataValueType.Any:
                    return (value ?? GetSqlText_Null());
                default:
                    return (value == null ? GetSqlText_Null() : GetSqlText_Text(value));
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
        public abstract string GetSqlText_True();

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
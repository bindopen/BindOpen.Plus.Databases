namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer
    {
        // Date and time

        /// <summary>
        /// Evaluates the script word $SQLGETCURRENTDATE.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public override string GetSqlText_CurrentDate()
        {
            return "now()";
        }
    }
}
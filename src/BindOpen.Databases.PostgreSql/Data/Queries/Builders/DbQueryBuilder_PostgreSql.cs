namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder_PostgreSql class.
        /// </summary>
        public DbQueryBuilder_PostgreSql() : base()
        {
            Id = "databases.postgresql$client";
        }

        #endregion
    }
}
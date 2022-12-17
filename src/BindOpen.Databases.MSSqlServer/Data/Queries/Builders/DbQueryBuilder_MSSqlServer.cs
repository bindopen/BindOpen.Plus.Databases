namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder_MSSqlServer class.
        /// </summary>
        public DbQueryBuilder_MSSqlServer() : base()
        {
            Id = "databases.postgresql$client";
        }

        #endregion
    }
}
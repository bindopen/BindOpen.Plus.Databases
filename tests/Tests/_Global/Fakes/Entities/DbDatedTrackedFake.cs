namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a dated database entity.
    /// </summary>
    public class DbDatedTrackedFake : DbDatedFake, IDbDatedTrackedFake
    {
        #region Properties

        /// <summary>
        /// The one who has created this instance.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// The one who has lastly modified this instance.
        /// </summary>
        public string LastModifiedBy { get; set; }

        #endregion
    }
}
namespace BindOpen.Tests.Databases.Data.Entities
{
    /// <summary>
    /// This interface defines a dated and tracked database entity.
    /// </summary>
    public interface IDbDatedTracked : IDbDated
    {
        #region Properties

        /// <summary>
        /// The one who has created this instance.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// The one who has lastly modified this instance.
        /// </summary>
        string LastModifiedBy { get; set; }

        #endregion
    }
}
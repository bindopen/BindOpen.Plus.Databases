using System;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This interface defines a dated database entity.
    /// </summary>
    public interface IDbDatedFake : IDbFake
    {
        #region Properties

        /// <summary>
        /// The creation date of this instance.
        /// </summary>
        DateTime? CreationDate { get; set; }

        /// <summary>
        /// The last modification date of this instance.
        /// </summary>
        DateTime? LastModificationDate { get; set; }

        #endregion
    }
}
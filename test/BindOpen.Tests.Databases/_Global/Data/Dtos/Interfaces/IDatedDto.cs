using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Dtos
{
    /// <summary>
    /// This interface defines dated DTO.
    /// </summary>
    public interface IDatedDto : IDto
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
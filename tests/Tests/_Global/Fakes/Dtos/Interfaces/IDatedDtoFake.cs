using BindOpen.Data;
using System;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This interface defines dated DTO.
    /// </summary>
    public interface IDatedDtoFake : IBdoDto
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
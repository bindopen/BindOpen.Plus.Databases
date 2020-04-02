using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Entities
{
    /// <summary>
    /// This interface defines a dated database entity.
    /// </summary>
    public interface IDbDated : IDb
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
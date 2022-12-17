using System;

namespace BindOpen.Databases.Tests.PostgreSql.Data.Entities
{
    /// <summary>
    /// This class represents a dated database entity.
    /// </summary>
    public class DbDated : IDbDated
    {
        #region Properties

        /// <summary>
        /// The creation date of this instance.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// The last modification date of this instance.
        /// </summary>
        public DateTime? LastModificationDate { get; set; }

        #endregion
    }
}
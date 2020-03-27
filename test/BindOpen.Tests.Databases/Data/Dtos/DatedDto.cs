using BindOpen.Data.Items;
using Newtonsoft.Json;
using System;

namespace BindOpen.Tests.Databases.Data.Dtos
{
    /// <summary>
    /// This class represents a dated DTO.
    /// </summary>
    public class DatedDto : DataItem, IDatedDto
    {
        #region Properties

        /// <summary>
        /// The creation date of this instance.
        /// </summary>
        [JsonProperty("creationDate")]
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// The last modification date of this instance.
        /// </summary>
        [JsonProperty("lastModificationDate")]
        public DateTime? LastModificationDate { get; set; }

        #endregion
    }
}
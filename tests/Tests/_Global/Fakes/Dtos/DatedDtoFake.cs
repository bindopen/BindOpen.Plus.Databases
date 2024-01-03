using Newtonsoft.Json;
using System;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a dated DTO.
    /// </summary>
    public class DatedDtoFake : IDatedDtoFake
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
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a community DTO.
    /// </summary>
    public class CommunityDtoFake : DatedDtoFake
    {
        #region Properties

        /// <summary>
        /// The code of this instance.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// The label of this instance.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// The codes of the countries of this instance.
        /// </summary>
        [JsonProperty("countryCodes")]
        public List<string> CountryCodes { get; set; } = new List<string>();

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CommunityDto class.
        /// </summary>
        public CommunityDtoFake()
        {
        }

        #endregion
    }
}
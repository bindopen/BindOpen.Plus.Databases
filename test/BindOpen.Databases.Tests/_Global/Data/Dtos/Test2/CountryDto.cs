using Newtonsoft.Json;

namespace BindOpen.Databases.Tests.PostgreSql.Data.Dtos.Test2
{
    /// <summary>
    /// This class represents a country DTO.
    /// </summary>
    public class CountryDto : DatedDto
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
        /// The information about this instance.
        /// </summary>
        [JsonProperty("info")]
        public CountryInformationDto Information { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CountryDto class.
        /// </summary>
        public CountryDto()
        {
        }

        #endregion
    }
}
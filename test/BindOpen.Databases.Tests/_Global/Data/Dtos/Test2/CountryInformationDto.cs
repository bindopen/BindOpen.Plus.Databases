using Newtonsoft.Json;


namespace BindOpen.Databases.Tests.PostgreSql.Data.Dtos.Test2
{
    /// <summary>
    /// This class represents a country information DTO.
    /// </summary>
    public class CountryInformationDto : DatedDto
    {
        #region Properties

        /// <summary>
        /// The country code of this instance.
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// The adjustment 1 of this instance.
        /// </summary>
        [JsonProperty("adjustment1")]
        public int Adjustment1 { get; set; }

        /// <summary>
        /// The delay 1 of this instance.
        /// </summary>
        [JsonProperty("delay1")]
        public int Delay1 { get; set; }

        /// <summary>
        /// The adjustment 2 of this instance.
        /// </summary>
        [JsonProperty("adjustment2")]
        public int Adjustment2 { get; set; }

        /// <summary>
        /// The delay 2 of this instance.
        /// </summary>
        [JsonProperty("delay2")]
        public int Delay2 { get; set; }

        /// <summary>
        /// The registry of this instance.
        /// </summary>
        [JsonProperty("registry")]
        public string Registry { get; set; }

        /// <summary>
        /// The international registry of this instance.
        /// </summary>
        [JsonProperty("internationalRegistry")]
        public string InternationalRegistry { get; set; }

        /// <summary>
        /// The start date of this instance.
        /// </summary>
        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CountryInformationDto class.
        /// </summary>
        public CountryInformationDto()
        {
        }

        #endregion
    }
}
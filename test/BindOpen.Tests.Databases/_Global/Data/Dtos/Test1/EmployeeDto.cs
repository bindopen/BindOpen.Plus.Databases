using Newtonsoft.Json;
using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Dtos.Test1
{
    /// <summary>
    /// This class represents a  employee DTO.
    /// </summary>
    public class EmployeeDto
    {
        #region Properties

        /// <summary>
        /// The code of this instance.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// The double field of this instance.
        /// </summary>
        [JsonProperty("double")]
        public double DoubleField { get; set; }

        /// <summary>
        /// The date time field of this instance.
        /// </summary>
        [JsonProperty("dateTime")]
        public DateTime? DateTimeField { get; set; }

        /// <summary>
        /// The integration date of this instance.
        /// </summary>
        [JsonProperty("long")]
        public long LongField { get; set; }

        /// <summary>
        /// The byte array of this instance.
        /// </summary>
        [JsonProperty("bytes")]
        public Byte[] ByteArrayField { get; set; }

        /// <summary>
        /// The reigonal diretorate code of this instance.
        /// </summary>
        [JsonProperty("regionalDirectorateCode")]
        public string RegionalDirectorateCode { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EmployeeDto class.
        /// </summary>
        public EmployeeDto()
        {
        }

        #endregion
    }
}
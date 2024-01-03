using System.Collections.Generic;

namespace BindOpen.Databases.Tests.Fakes.Test2
{
    /// <summary>
    /// This class represents a country in database.
    /// </summary>
    public class DbCountryFake : DbDatedFake
    {
        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// The code of this instance.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The FR label of this instance.
        /// </summary>
        public string LabelFR { get; set; }

        /// <summary>
        /// The EN label of this instance.
        /// </summary>
        public string LabelEN { get; set; }

        // Bound entities --------------------------------

        /// <summary>
        /// The information about this instance.
        /// </summary>
        public DbCountryInformationFake Information { get; set; }

        /// <summary>
        /// The country / community relationships bound to this instance.
        /// </summary>
        public IEnumerable<DbCountry_CommunityFake> Country_Communitys { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CountryDto class.
        /// </summary>
        public DbCountryFake()
        {
        }

        #endregion
    }
}
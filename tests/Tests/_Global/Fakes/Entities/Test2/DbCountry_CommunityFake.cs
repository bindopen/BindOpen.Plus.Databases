namespace BindOpen.Databases.Tests.Fakes.Test2
{
    /// <summary>
    /// This class represents a country/community relationship in database.
    /// </summary>
    public class DbCountry_CommunityFake
    {
        #region Properties

        /// <summary>
        /// The ID of the country of this instance.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// The ID of the community of this instance.
        /// </summary>
        public int CommunityId { get; set; }

        // Bound entities --------------------------------

        /// <summary>
        /// The country of this instance.
        /// </summary>
        public DbCountryFake Country { get; set; }

        /// <summary>
        /// The community of this instance.
        /// </summary>
        public DbCommunityFake Community { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbCountry_Community class.
        /// </summary>
        public DbCountry_CommunityFake()
        {
        }

        #endregion
    }
}
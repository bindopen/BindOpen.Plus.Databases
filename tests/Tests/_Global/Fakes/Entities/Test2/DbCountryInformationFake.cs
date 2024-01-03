namespace BindOpen.Databases.Tests.Fakes.Test2
{
    /// <summary>
    /// This class represents a country information in database.
    /// </summary>
    public class DbCountryInformationFake : DbDatedFake
    {
        #region Properties

        /// <summary>
        /// The country ID of this instance.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// The start date of this instance.
        /// </summary>
        public string StartDate { get; set; }

        // Bound entities --------------------------------

        /// <summary>
        /// The country bound to this instance.
        /// </summary>
        public DbCountryFake Country { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the CountryInformationDb class.
        /// </summary>
        public DbCountryInformationFake()
        {
        }

        #endregion
    }
}
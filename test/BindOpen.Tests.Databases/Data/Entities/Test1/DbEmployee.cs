using BindOpen.Extensions.Carriers;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Entities.Test1
{
    /// <summary>
    /// This class represents an employee in database.
    /// </summary>
    [BdoDbTable(nameof(DbEmployee), "Test1")]
    public class DbEmployee
    {
        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The OD of the regional direction of this instance.
        /// </summary>
        public int RegionalDirectorateId { get; set; }


        /// <summary>
        /// The code of this instance.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The first name of this instance.
        /// </summary>
        public string FisrtName { get; set; }

        /// <summary>
        /// The last name of this instance.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The integration date of this instance.
        /// </summary>
        public string IntegrationDate { get; set; }

        /// <summary>
        /// The contact email of this instance.
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// The length of this instance.
        /// </summary>
        public long Length { get; set; }

        // Bound entities ------------------------

        /// <summary>
        /// 
        /// </summary>
        public DbRegionalDirectorate RegionalDirectorate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EmployeeDb class.
        /// </summary>
        public DbEmployee()
        {
        }

        #endregion
    }
}
using BindOpen.Extensions.Carriers;
using System;

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
        /// The ID of the regional direction of this instance.
        /// </summary>
        public int RegionalDirectorateId { get; set; }

        /// <summary>
        /// The ID of the main contact of this instance.
        /// </summary>
        public int MainContactId { get; set; }

        /// <summary>
        /// The ID of the secondary contact of this instance.
        /// </summary>
        public int SecondaryContactId { get; set; }

        /// <summary>
        /// The code of this instance.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The double field of this instance.
        /// </summary>
        public double DoubleField { get; set; }

        /// <summary>
        /// The date time field of this instance.
        /// </summary>
        public DateTime DateTimeField { get; set; }

        /// <summary>
        /// The integration date of this instance.
        /// </summary>
        public long LongField { get; set; }

        /// <summary>
        /// The byte array of this instance.
        /// </summary>
        public Byte[] ByteArrayField { get; set; }

        // Bound entities ------------------------

        /// <summary>
        /// 
        /// </summary>
        public DbRegionalDirectorate RegionalDirectorate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbContact MainContact { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbContact SecondaryContact { get; set; }

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
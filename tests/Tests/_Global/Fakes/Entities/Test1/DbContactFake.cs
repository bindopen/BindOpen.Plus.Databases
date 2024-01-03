using System.Collections.Generic;

namespace BindOpen.Databases.Tests.Fakes.Test1
{
    /// <summary>
    /// This class represents a regional direction in database.
    /// </summary>
    public class DbContactFake
    {
        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        public int ContactId { get; set; }

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

        // Bound entities ------------------------

        /// <summary>
        /// 
        /// </summary>
        public List<DbContactFake> EmployeesAsMain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DbContactFake> EmployeesAsSecondary { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbContact class.
        /// </summary>
        public DbContactFake()
        {
        }

        #endregion
    }
}
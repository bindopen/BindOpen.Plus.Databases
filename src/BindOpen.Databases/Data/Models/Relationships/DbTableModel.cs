using BindOpen.Data.Items;
using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Data.Models
{
    /// <summary>
    /// This class represents the table model.
    /// </summary>
    public class DbTableModel : DataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The table of this instance.
        /// </summary>
        public DbTable Table { get; set; }

        /// <summary>
        /// The fields of this instance.
        /// </summary>
        public List<DbField> Fields { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbTableModel class.
        /// </summary>
        public DbTableModel()
        {
        }

        #endregion
    }
}
using BindOpen.Data.Items;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Models
{
    /// <summary>
    /// This class represents the table relationship.
    /// </summary>
    public class DbTableRelationship : DataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The table 1 of this instance.
        /// </summary>
        public DbTable Table1 { get; set; }

        /// <summary>
        /// The table 2 of this instance.
        /// </summary>
        public DbTable Table2 { get; set; }

        /// <summary>
        /// The field mapping of this instance.
        /// </summary>
        public DictionaryDataItem FieldMappingDictionary { get; set; } = new DictionaryDataItem();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbTableRelationship class.
        /// </summary>
        public DbTableRelationship()
        {
        }

        #endregion
    }
}
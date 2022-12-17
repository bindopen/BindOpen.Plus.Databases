using BindOpen.Databases.Data;
using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents the table relationship.
    /// </summary>
    public class DbTableRelationship : DataItem, IDbTableRelationship
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The table 1 of this instance.
        /// </summary>
        public IDbTable Table1 { get; set; }

        /// <summary>
        /// The table 2 of this instance.
        /// </summary>
        public IDbTable Table2 { get; set; }

        /// <summary>
        /// The field mapping of this instance.
        /// </summary>
        public IDictionaryDataItem FieldMappingDictionary { get; set; } = new DictionaryDataItem();

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


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone<IDbTableRelationship>(areas);
            clone.Table1 = Table1?.Clone<IDbTable>();
            clone.Table2 = Table2?.Clone<IDbTable>();
            clone.FieldMappingDictionary = FieldMappingDictionary?.Clone<DictionaryDataItem>();

            return clone;
        }

        #endregion
    }
}
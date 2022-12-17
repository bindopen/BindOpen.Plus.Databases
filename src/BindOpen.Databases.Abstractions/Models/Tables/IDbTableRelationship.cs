using BindOpen.Databases.Data;
using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents the table relationship.
    /// </summary>
    public interface IDbTableRelationship : IDataItem
    {
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
        public IDictionaryDataItem FieldMappingDictionary { get; set; }
    }
}
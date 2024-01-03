using BindOpen.Data;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents the table relationship.
    /// </summary>
    public interface IDbTableRelationship : IBdoObject
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
        public ITBdoDictionary<string> FieldMappingDictionary { get; set; }
    }
}
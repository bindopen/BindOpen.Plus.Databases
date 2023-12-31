using BindOpen.Data;
using BindOpen.Plus.Databases.Models;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// This class represents the table relationship.
    /// </summary>
    public class DbTableRelationship : BdoObject, IDbTableRelationship
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
        public ITBdoDictionary<string> FieldMappingDictionary { get; set; }

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
        public override object Clone()
        {
            var clone = base.Clone<IDbTableRelationship>();
            clone.Table1 = Table1?.Clone<IDbTable>();
            clone.Table2 = Table2?.Clone<IDbTable>();
            clone.FieldMappingDictionary = FieldMappingDictionary?.Clone<TBdoDictionary<string>>();

            return clone;
        }

        #endregion
    }
}
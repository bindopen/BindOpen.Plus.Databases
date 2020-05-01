using BindOpen.Data.Items;
using BindOpen.Databases.Data.Queries;
using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Databases.Data.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public abstract partial class BdoDbModel : IdentifiedDataItem, IBdoDbModel, IBdoDbModelBuilder
    {
        #region Variables

        // Properties ---------------------------------------

        internal Dictionary<string, DbTableModel> TableModelDictionary = new Dictionary<string, DbTableModel>();
        internal Dictionary<string, DbTableRelationship> TableRelationShipDictionary = new Dictionary<string, DbTableRelationship>();
        internal Dictionary<string, DbField[]> TupleDictionary = new Dictionary<string, DbField[]>();
        internal Dictionary<string, IDbStoredQuery> QueryDictionary = new Dictionary<string, IDbStoredQuery>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected BdoDbModel()
        {
            OnCreating();
        }

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public virtual void OnCreating()
        {
        }

        #endregion
    }
}

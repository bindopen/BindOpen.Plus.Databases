using System.Collections.Generic;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public abstract partial class BdoDbRelationalModel :
        BdoDbModel, IBdoDbRelationalModel, IBdoDbRelationalModelBuilder
    {
        #region Variables

        internal Dictionary<string, IDbTableModel> TableModelDictionary = new Dictionary<string, IDbTableModel>();
        internal Dictionary<string, IDbTableRelationship> TableRelationShipDictionary = new Dictionary<string, IDbTableRelationship>();
        internal Dictionary<string, IDbField[]> TupleDictionary = new Dictionary<string, IDbField[]>();
        internal Dictionary<string, IDbStoredQuery> QueryDictionary = new Dictionary<string, IDbStoredQuery>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected BdoDbRelationalModel()
        {
            OnCreating();
        }

        #endregion
    }
}

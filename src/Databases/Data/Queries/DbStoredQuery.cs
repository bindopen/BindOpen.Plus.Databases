using System.Collections.Generic;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// This class represents a stored data query.
    /// </summary>
    public class DbStoredQuery : DbQuery, IDbStoredQuery
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbStoredQuery class.
        /// </summary>
        public DbStoredQuery() : base(DbQueryKind.None)
        {
        }

        #endregion

        // ------------------------------------------
        // IDbStoredQuery Implementation
        // ------------------------------------------

        #region IDbStoredQuery

        /// <summary>
        /// Gets the key of the item.
        /// </summary>
        /// <returns>Returns the key of the item.</returns>
        public override string Key() => Name;

        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        /// <returns>Returns the name of this instance.</returns>
        /// <remarks>If the name of this instance is empty or null then the returned name is determined from this instance's properties.</remarks>
        public override string GetName()
        {
            var st = base.GetName();

            if (string.IsNullOrEmpty(st))
            {
                st = Query?.GetName();
            }

            return st;
        }

        /// <summary>
        /// The query of this instance.
        /// </summary>
        public IDbQuery Query { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IDbStoredQuery WithQuery(IDbStoredQuery query)
        {
            Query = query;
            return this;
        }

        /// <summary>
        /// The SQL query text of this instance.
        /// </summary>
        public Dictionary<string, string> QueryTexts { get; set; }

        #endregion

        // ------------------------------------------
        // IBdoObject Implementation
        // ------------------------------------------

        #region IBdoObject

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone()
        {
            var clone = base.Clone() as DbStoredQuery;
            clone.Query = Query?.Clone<IDbQuery>();

            if (QueryTexts != null)
            {
                foreach (var item in QueryTexts)
                {
                    clone.QueryTexts.Add(item.Key, item.Value);
                }
            }

            return clone;
        }

        #endregion
    }
}
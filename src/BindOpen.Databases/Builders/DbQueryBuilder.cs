using BindOpen.Databases.Data;
using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Elements;
using BindOpen.Framework.MetaData.Items;
using BindOpen.Framework.MetaData.Stores;
using BindOpen.Framework.Runtime.Scopes;
using BindOpen.Logging;
using System;

namespace BindOpen.Databases.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public abstract partial class DbQueryBuilder : DataItem,
        IDbQueryBuilder
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryBuilder class.
        /// </summary>
        protected DbQueryBuilder()
        {
        }

        #endregion

        // ------------------------------------------
        // IDbQueryBuilder Implementation
        // ------------------------------------------

        #region IDbQueryBuilder

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public IDbQueryBuilder WithScope(IBdoScope scope)
        {
            Scope = scope;
            return this;
        }

        #endregion

        // ------------------------------------------
        // ITIdentifiedPoco Implementation
        // ------------------------------------------

        #region ITIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IDbQueryBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbQueryBuilder Implementation
        // ------------------------------------------

        #region IDbQueryBuilder

        /// <summary>
        /// Gets the database name corresponding to the specified data module name.
        /// </summary>
        /// <param name="dataModuleName">The data module name to consider.</param>
        /// <remarks>If not found, it returns the specified data module name.</remarks>
        protected string GetDatabaseName(string dataModuleName)
        {
            var dataSourceDepot = Scope?.DataStore?.Get<IBdoDatasourceDepot>();
            if (dataSourceDepot == null)
                return dataModuleName;
            else
            {
                var databaseName = dataSourceDepot.GetInstanceOtherwiseModuleName(dataModuleName);
                if (databaseName == StringHelper.__NoneString)
                {
                    databaseName = dataModuleName;
                }
                return databaseName;
            }
        }

        /// <summary>
        /// Updates the specified parameter set with the specified query.
        /// </summary>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="query">The query to consider.</param>
        protected static void UpdateParameterSet(IDataElementSet parameterSet, IDbQuery query)
        {
            parameterSet?.Update(query?.ParameterSpecSet);
            parameterSet?.Update(query?.ParameterSet);
        }

        /// <summary>
        /// Builds the SQL text from the specified database query.
        /// </summary>
        /// <param name="query">The database data query to build.</param>
        /// <param name="parameterMode">The display mode of parameters to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varElementSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        public string BuildQuery(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IDataElementSet parameterSet = null,
            IDataElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var queryString = "";

            if (query != null)
            {
                try
                {
                    if (query is IDbSingleQuery singleDbQuery)
                    {
                        this.AddInto(varElementSet);
                        queryString = GetSqlText_Query(singleDbQuery, parameterSet, varElementSet, log);
                    }
                    else if (query is IDbCompositeQuery compositeDbQuery)
                    {
                        this.AddInto(varElementSet);
                        queryString = GetSqlText_Query(compositeDbQuery, parameterSet, varElementSet, log);
                    }
                    else if (query is IDbStoredQuery storedDbQuery)
                    {
                        if (!storedDbQuery.QueryTexts.TryGetValue(Id, out queryString))
                        {
                            queryString = BuildQuery(storedDbQuery.Query, DbQueryParameterMode.Scripted, parameterSet, varElementSet, log);
                            storedDbQuery.QueryTexts.Add(Id, queryString);
                        }
                    }

                    if (query.SubQueries != null)
                    {
                        for (int i = 0; i < query.SubQueries.Count; i++)
                        {
                            var subQuery = query.SubQueries[i];
                            var subQueryString = BuildQuery(subQuery, parameterMode, parameterSet, varElementSet, log);

                            queryString = queryString.Replace((i + 1).ToString().AsQueryWildString(), subQueryString);
                        }
                    }

                    if (parameterMode != DbQueryParameterMode.Scripted)
                    {
                        parameterSet ??= new DataElementSet();
                        UpdateParameterSet(parameterSet, query);

                        if (query is IDbStoredQuery storedDbQuery)
                        {
                            UpdateParameterSet(parameterSet, storedDbQuery.Query);
                        }

                        if (parameterSet?.Items != null)
                        {
                            foreach (var parameter in parameterSet.Items)
                            {
                                if (parameterMode == DbQueryParameterMode.ValueInjected)
                                {
                                    queryString = queryString.Replace((parameter?.Name ?? parameter.Index.ToString())?.AsParameterWildString(),
                                        GetSqlText_Value(parameter?.GetItem(Scope, varElementSet, log), parameter.ValueType));
                                }
                                else
                                {
                                    queryString = queryString.Replace((parameter?.Name ?? parameter.Index.ToString())?.AsParameterWildString(),
                                        "@" + parameter?.Name ?? parameter.Index.ToString());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log?.AddError(
                        "Error trying to build query '" + (query?.Name ?? "(Undefinied)") + "'",
                        description: ex.ToString() + ". Built query is : '" + queryString + "'.");
                }
            }

            return queryString;
        }

        // Builds single query ----------------------

        /// <summary>
        /// Builds the SQL text of the specified basic query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected abstract string GetSqlText_Query(
            IDbSingleQuery query,
            IDataElementSet parameterSet = null,
            IDataElementSet varElementSet = null,
            IBdoLog log = null);

        // Builds merge query ----------------------

        /// <summary>
        /// Builds the SQL text of the specified merge query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected abstract string GetSqlText_Query(
            IDbCompositeQuery query,
            IDataElementSet parameterSet = null,
            IDataElementSet varElementSet = null,
            IBdoLog log = null);

        #endregion

        // --------------------------------------------------
        // IDisposable IMPLEMENTATION
        // --------------------------------------------------

        #region IDisposable Implementation

        /// <summary>
        /// Disposes specifying whether this instance is disposing.
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                Scope?.Dispose();
            }
        }

        #endregion
    }
}
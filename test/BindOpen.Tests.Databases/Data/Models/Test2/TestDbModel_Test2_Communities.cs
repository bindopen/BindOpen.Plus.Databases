using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Models;
using BindOpen.Data.Queries;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.Data.Dtos.Test2;
using BindOpen.Tests.Databases.Data.Entities.Test1;
using BindOpen.Tests.Databases.Data.Entities.Test2;

namespace BindOpen.Tests.Databases.Data.Models
{
    /// <summary>
    /// This class represents a test database model.
    /// </summary>
    public partial class TestDbModel : BdoDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="q"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageToken"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        internal IDbQuery QuerySelectCommunities(
            string q, string orderBy, int? pageSize = null, string pageToken = null,
            IBdoLog log = null)
        {
            return this.UseQuery("GetCommunityWithCode", p =>
                DbFluent.SelectQuery(Table<DbCommunity>("community"))
                    .From(
                        DbFluent.Table(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>(),
                            JoinCondition("Community_RegionalDirectorate")))
                    .WithFields(Tuple("SelectCommunity"))
                    .Filter(
                        q,
                        DbApiFluent.CreateFilterDefinition(
                            DbApiFluent.CreateFilterClause("startDate", Field<DbCommunity>(p => p.CreationDate), DataOperator.GreaterOrEqual),
                            DbApiFluent.CreateFilterClause("endDate", Field<DbCommunity>(p => p.LastModificationDate), DataOperator.LesserOrEqual),
                            DbApiFluent.CreateFilterClause("code", Field<DbCommunity>(p => p.Code), DataOperator.Equal)),
                        log)
                    .Sort(
                        orderBy,
                        DbApiFluent.CreateSortDefinition(
                            DbApiFluent.CreateSortClause("startDate", Field<DbCommunity>(p => p.CreationDate)),
                            DbApiFluent.CreateSortClause("endDate", Field<DbCommunity>(p => p.LastModificationDate)),
                            DbApiFluent.CreateSortClause("code", Field<DbCommunity>(p => p.Code))),
                        log));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery QuerySelectCommunityWithCode(string code)
        {
            return this.UseQuery("GetCommunityWithCode", p =>
                DbFluent.SelectQuery(Table<DbCommunity>("community"))
                    .From(
                        DbFluent.Table(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>(),
                            JoinCondition("Community_RegionalDirectorate")))
                    .WithFields(Tuple("SelectCommunity"))
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbCommunity.Code), q.UseParameter("code", DataValueType.Text))))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="community"></param>
        /// <returns></returns>
        internal IDbQuery QueryInsertCommunity(CommunityDto community)
        {
            return DbFluent.InsertQuery(Table("Community"), true)
                .From(
                    DbFluent.Table(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>(),
                        JoinCondition<DbCommunity, DbRegionalDirectorate>("community", null)));
            //.WithFields(p => Fields_InsertCommunity(p, community));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isPartialUpdate"></param>
        /// <param name="community"></param>
        /// <returns></returns>
        internal IDbQuery QueryUpdateCommunity(string code, bool isPartialUpdate, CommunityDto community)
        {
            var query = DbFluent.UpdateQuery(Table<DbCommunity>("community"))
                .From(
                    DbFluent.Table(DbQueryJoinKind.Left, Table("RegionalDirectorate", "directorate"),
                        JoinCondition("Community_RegionalDirectorate")));

            if (!isPartialUpdate || community?.Code?.Length > 0)
            {
                query.AddField(p => DbFluent.FieldAsParameter(nameof(DbCommunity.Code), p.UseParameter("code", nameof(DbCommunity.Code))));
            }

            return query;
        }

        /// <summary>
        /// Delete the specified community.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery QueryDeleteCommunity(string code)
        {
            return this.UseQuery("DeleteCommunity", p =>
                DbFluent.DeleteQuery(Table("Community"))
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbCommunity.Code), q.UseParameter("code", DataValueType.Text))))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }
    }
}

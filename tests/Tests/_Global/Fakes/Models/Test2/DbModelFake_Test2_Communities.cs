using BindOpen.Data;
using BindOpen.Databases.Relational;
using BindOpen.Databases.Tests.Fakes.Test1;
using BindOpen.Databases.Tests.Fakes.Test2;
using BindOpen.Logging;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a test database model.
    /// </summary>
    public partial class DbModelFake : BdoDbRelationalModel
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
                BdoDb.SelectQuery(Table<DbCommunityFake>("community"))
                    .From(
                        BdoDb.TableAsJoin(DbQueryJoinKind.Left, Table("RegionalDirectorate"),
                            JoinCondition("Community_RegionalDirectorate")))
                    .WithFields(Tuple("SelectCommunity"))
                    .Filter(
                        q,
                        BdoDb.CreateFilterDefinition(
                            BdoDb.CreateFilterClause("startDate", Field<DbCommunityFake>(p => p.CreationDate), DataOperators.GreaterOrEqualThan),
                            BdoDb.CreateFilterClause("endDate", Field<DbCommunityFake>(p => p.LastModificationDate), DataOperators.LesserOrEqualThan),
                            BdoDb.CreateFilterClause("code", Field<DbCommunityFake>(p => p.Code), DataOperators.EqualsTo)),
                        log)
                    .Sort(
                        orderBy,
                        BdoDb.CreateSortDefinition(
                            BdoDb.CreateSortClause("startDate", Field<DbCommunityFake>(p => p.CreationDate)),
                            BdoDb.CreateSortClause("endDate", Field<DbCommunityFake>(p => p.LastModificationDate)),
                            BdoDb.CreateSortClause("code", Field<DbCommunityFake>(p => p.Code))),
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
                BdoDb.SelectQuery(Table<DbCommunityFake>("community"))
                    .From(
                        BdoDb.TableAsJoin(DbQueryJoinKind.Left, Table("RegionalDirectorate"),
                            JoinCondition("Community_RegionalDirectorate")))
                    .WithFields(Tuple("SelectCommunity"))
                    .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbCommunityFake.Code), q.UseParameter("code", DataValueTypes.Text))))
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="community"></param>
        /// <returns></returns>
        internal IDbQuery QueryInsertCommunity(CommunityDtoFake community)
        {
            return BdoDb.InsertQuery(Table("Community"), true)
                .From(
                    BdoDb.TableAsJoin(DbQueryJoinKind.Left, Table("RegionalDirectorate"),
                        JoinCondition<DbCommunityFake, DbRegionalDirectorateFake>("community", null)));
            //.WithFields(p => Fields_InsertCommunity(p, community));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isPartialUpdate"></param>
        /// <param name="community"></param>
        /// <returns></returns>
        internal IDbQuery QueryUpdateCommunity(string code, bool isPartialUpdate, CommunityDtoFake community)
        {
            var query = BdoDb.UpdateQuery(Table<DbCommunityFake>("community"))
                .From(
                    BdoDb.TableAsJoin(DbQueryJoinKind.Left, Table("RegionalDirectorate", "directorate"),
                        JoinCondition("Community_RegionalDirectorate")));

            if (!isPartialUpdate || community?.Code?.Length > 0)
            {
                query.AddField(p => BdoDb.FieldAsParameter(nameof(DbCommunityFake.Code), p.UseParameter("code", nameof(DbCommunityFake.Code))));
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
                BdoDb.DeleteQuery(Table("Community"))
                    .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbCommunityFake.Code), q.UseParameter("code", DataValueTypes.Text))))
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }
    }
}

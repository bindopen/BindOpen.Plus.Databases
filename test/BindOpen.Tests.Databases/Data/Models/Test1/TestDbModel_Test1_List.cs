using BindOpen.Data.Common;
using BindOpen.Databases.Data.Models;
using BindOpen.Databases.Data.Queries;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.Data.Entities.Test1;

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
        internal IDbQuery ListEmployees1(
            string q, string orderBy, int? pageSize = null, string pageToken = null,
            IBdoLog log = null)
            => DbFluent.SelectQuery("GetMyTables", DbFluent.Table())
                .WithLimit(100)
                .AsDistinct()
                .WithFields(
                    DbFluent.FieldAsAll(DbFluent.Table("table")),
                    DbFluent.Field("Field1", DbFluent.Table("table")),
                    DbFluent.Field("Field2", DbFluent.Table("table")))
                .From(
                    DbFluent.Table(nameof(DbRegionalDirectorate).Substring(2), "schema1").WithAlias("table"),
                    DbFluent.Table(DbQueryJoinKind.Left, DbFluent.Table("DbTable1".Substring(2), "schema2").WithAlias("table1"))
                        .WithCondition(
                            DbFluent.And(
                                DbFluent.Eq(
                                    DbFluent.Field("table1key", DbFluent.Table("table1")),
                                    DbFluent.Field(nameof(DbRegionalDirectorate.Code), DbFluent.Table("table"))),
                                DbFluent.Eq(
                                    DbFluent.Field("table2key", DbFluent.Table("table2")),
                                    DbFluent.Field(nameof(DbRegionalDirectorate.LabelEN), DbFluent.Table("table"))))),
                    DbFluent.Table(DbQueryJoinKind.Left, DbFluent.Table("DbTable1".Substring(2), "schema2").WithAlias("table2"))
                        .WithCondition(
                            DbFluent.Eq(
                                DbFluent.Field("table1key", DbFluent.Table("table2")),
                                DbFluent.Field("Field1", DbFluent.Table("table"))))
                )
                .Filter(
                    q,
                    DbApiFluent.CreateFilterDefinition(
                        DbApiFluent.CreateFilterClause("startDate", DbFluent.Field("CreationDate", DbFluent.Table("table")), DataOperator.GreaterOrEqual),
                        DbApiFluent.CreateFilterClause("endDate", DbFluent.Field("LastModificationDate", DbFluent.Table("table")), DataOperator.LesserOrEqual),
                        DbApiFluent.CreateFilterClause("code", DbFluent.Field("Code", DbFluent.Table("table")), DataOperator.Equal)),
                    log)
                .Sort(
                    orderBy,
                    DbApiFluent.CreateSortDefinition(
                        DbApiFluent.CreateSortClause("startDate", DbFluent.Field("CreationDate", DbFluent.Table("table"))),
                        DbApiFluent.CreateSortClause("endDate", DbFluent.Field("LastModificationDate", DbFluent.Table("table"))),
                        DbApiFluent.CreateSortClause("code", DbFluent.Field("Code", DbFluent.Table("table")))),
                    log);
    }
}

using BindOpen.Data;
using BindOpen.Databases.Models;
using BindOpen.Databases.Tests.Fakes.Test1;
using BindOpen.Logging;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a test database model.
    /// </summary>
    public partial class DbModelFake : BdoDbModel
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
            => BdoDb.SelectQuery("GetMyTables", BdoDb.Table())
                .WithLimit(100)
                .AsDistinct()
                .WithFields(
                    BdoDb.FieldAsAll(BdoDb.Table("table")),
                    BdoDb.Field("Field1", BdoDb.Table("table")),
                    BdoDb.Field("Field2", BdoDb.Table("table")))
                .From(
                    BdoDb.Table(nameof(DbRegionalDirectorateFake).Substring(2), "schema1").WithAlias("table"),
                    BdoDb.TableAsJoin(
                        DbQueryJoinKind.Left,
                        BdoDb.Table("DbTable1".Substring(2), "schema2").WithAlias("table1"),
                        BdoDb.And(
                            BdoDb.Eq(
                                BdoDb.Field("table1key", BdoDb.Table("table1")),
                                BdoDb.Field(nameof(DbRegionalDirectorateFake.Code), BdoDb.Table("table"))),
                            BdoDb.Eq(
                                BdoDb.Field("table2key", BdoDb.Table("table2")),
                                BdoDb.Field(nameof(DbRegionalDirectorateFake.LabelEN), BdoDb.Table("table"))))),
                    BdoDb.TableAsJoin(
                        DbQueryJoinKind.Left,
                        BdoDb.Table("DbTable1".Substring(2), "schema2").WithAlias("table2"),
                        BdoDb.Eq(
                            BdoDb.Field("table1key", BdoDb.Table("table2")),
                            BdoDb.Field("Field1", BdoDb.Table("table"))))
                )
                .Filter(
                    q,
                    BdoDb.CreateFilterDefinition(
                        BdoDb.CreateFilterClause("startDate", BdoDb.Field("CreationDate", BdoDb.Table("table")), DataOperators.GreaterOrEqualThan),
                        BdoDb.CreateFilterClause("endDate", BdoDb.Field("LastModificationDate", BdoDb.Table("table")), DataOperators.LesserOrEqualThan),
                        BdoDb.CreateFilterClause("code", BdoDb.Field("Code", BdoDb.Table("table")), DataOperators.EqualsTo)),
                    log)
                .Sort(
                    orderBy,
                    BdoDb.CreateSortDefinition(
                        BdoDb.CreateSortClause("startDate", BdoDb.Field("CreationDate", BdoDb.Table("table"))),
                        BdoDb.CreateSortClause("endDate", BdoDb.Field("LastModificationDate", BdoDb.Table("table"))),
                        BdoDb.CreateSortClause("code", BdoDb.Field("Code", BdoDb.Table("table")))),
                    log);
    }
}

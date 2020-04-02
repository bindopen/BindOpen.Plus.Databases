using BindOpen.Databases.Data.Models;
using BindOpen.Databases.Data.Queries;
using BindOpen.Tests.Databases.PostgreSql.Data.Entities.Test1;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Models
{
    /// <summary>
    /// This class represents a test database model.
    /// </summary>
    public partial class TestDbModel : BdoDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void OnCreating_Test1()
        {
            AddTable("Employee", DbFluent.Table(nameof(DbEmployee).Substring(2), "Mdm"))
                .AddTable<DbRegionalDirectorate>(DbFluent.Table(nameof(DbRegionalDirectorate).Substring(2), "Mdm"))

                .AddRelationship("Employee_RegionalDirectorate",
                    Table("Employee"), Table<DbRegionalDirectorate>(),
                    (nameof(DbEmployee.EmployeeId), nameof(DbRegionalDirectorate.RegionalDirectorateId)))

                .AddTuple("Fields_SelectEmployee",
                    DbFluent.FieldAsAll(Table("Employee")),
                    DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId), Table<DbRegionalDirectorate>()),
                    DbFluent.Field(nameof(DbRegionalDirectorate.Code), Table<DbRegionalDirectorate>()))

                .AddQuery(
                    DbFluent.StoredQuery(
                        DbFluent.SelectQuery(Table("Employee"))
                        .From(
                            Table("Employee"),
                            DbFluent.TableAsJoin(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>())
                                .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                        .WithFields(Tuple("Fields_SelectEmployee"))
                        .AddIdField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), p.UseParameter("code")))));
        }
    }
}

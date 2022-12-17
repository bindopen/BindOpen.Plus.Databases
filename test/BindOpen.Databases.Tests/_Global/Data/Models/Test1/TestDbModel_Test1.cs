using BindOpen.Databases.Models;
using BindOpen.Databases.Data;
using BindOpen.Databases.Tests.PostgreSql.Data.Entities.Test1;

namespace BindOpen.Databases.Tests.PostgreSql.Data.Models
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
            AddTable<DbEmployee>(DbFluent.Table(nameof(DbEmployee).Substring(2), "Mdm"))
            .AddTable("RegionalDirectorate", DbFluent.Table(nameof(DbRegionalDirectorate).Substring(2), "Mdm"))
            .AddTable<DbContact>(DbFluent.Table(nameof(DbContact).Substring(2), "Mdm"))

            .AddRelationship("Employee_RegionalDirectorate",
                Table<DbEmployee>(), Table("RegionalDirectorate"),
                (nameof(DbEmployee.EmployeeId), nameof(DbRegionalDirectorate.RegionalDirectorateId)))

            .AddRelationship<DbEmployee, DbContact>((p => p.MainContactId, q => q.ContactId))
            .AddRelationship<DbEmployee, DbContact>("secondary", (p => p.MainContactId, q => q.ContactId))

            .AddTuple("Fields_SelectEmployee",
                DbFluent.FieldAsAll(Table<DbEmployee>()),
                DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId), Table("RegionalDirectorate")),
                DbFluent.Field(nameof(DbRegionalDirectorate.Code), Table("RegionalDirectorate")))

            .AddTuple("Fields_SelectEmployee2",
                DbFluent.FieldAsAll(Table<DbEmployee>("employee")),
                DbFluent.Field<DbContact>(p => p.Code, Table<DbContact>("contact")).WithAlias("contactCode"),
                DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId), Table("RegionalDirectorate", "regionalDirectorate")),
                DbFluent.Field(nameof(DbRegionalDirectorate.Code), Table("RegionalDirectorate", "regionalDirectorate")))

            .AddQuery(
                DbFluent.StoredQuery(
                    DbFluent.SelectQuery(Table<DbEmployee>())
                    .From(
                        Table<DbEmployee>(),
                        DbFluent.TableAsJoin(
                            DbQueryJoinKind.Left,
                            Table("RegionalDirectorate"),
                            JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .AddIdField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), p.UseParameter("code")))));
        }
    }
}

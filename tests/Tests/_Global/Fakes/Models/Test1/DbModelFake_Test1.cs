using BindOpen.Databases.Models;
using BindOpen.Databases.Tests.Fakes.Test1;

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
        /// <param name="builder"></param>
        public void OnCreating_Test1()
        {
            AddTable<DbEmployeeFake>(BdoDb.Table(nameof(DbEmployeeFake)[2..-4], "Mdm"))
            .AddTable("RegionalDirectorate", BdoDb.Table(nameof(DbRegionalDirectorateFake).Substring(2), "Mdm"))
            .AddTable<DbContactFake>(BdoDb.Table(nameof(DbContactFake).Substring(2), "Mdm"))

            .AddRelationship("Employee_RegionalDirectorate",
                Table<DbEmployeeFake>(), Table("RegionalDirectorate"),
                (nameof(DbEmployeeFake.EmployeeId), nameof(DbRegionalDirectorateFake.RegionalDirectorateId)))

            .AddRelationship<DbEmployeeFake, DbContactFake>((p => p.MainContactId, q => q.ContactId))
            .AddRelationship<DbEmployeeFake, DbContactFake>("secondary", (p => p.MainContactId, q => q.ContactId))

            .AddTuple("Fields_SelectEmployee",
                BdoDb.FieldAsAll(Table<DbEmployeeFake>()),
                BdoDb.Field(nameof(DbRegionalDirectorateFake.RegionalDirectorateId), Table("RegionalDirectorate")),
                BdoDb.Field(nameof(DbRegionalDirectorateFake.Code), Table("RegionalDirectorate")))

            .AddTuple("Fields_SelectEmployee2",
                BdoDb.FieldAsAll(Table<DbEmployeeFake>("employee")),
                BdoDb.Field<DbContactFake>(p => p.Code, Table<DbContactFake>("contact")).WithAlias("contactCode"),
                BdoDb.Field(nameof(DbRegionalDirectorateFake.RegionalDirectorateId), Table("RegionalDirectorate", "regionalDirectorate")),
                BdoDb.Field(nameof(DbRegionalDirectorateFake.Code), Table("RegionalDirectorate", "regionalDirectorate")))

            .AddQuery(
                BdoDb.StoredQuery(
                    BdoDb.SelectQuery(Table<DbEmployeeFake>())
                    .From(
                        Table<DbEmployeeFake>(),
                        BdoDb.TableAsJoin(
                            DbQueryJoinKind.Left,
                            Table("RegionalDirectorate"),
                            JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .AddIdField(p => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), p.UseParameter("code")))));
        }
    }
}

using BindOpen.Data.Models;
using BindOpen.Data.Queries;
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
        /// <param name="builder"></param>
        public void OnCreating_Test1(IBdoDbModelBuilder builder)
        {
            builder
                .AddTable("Employee", DbFluent.Table(nameof(DbEmployee).Substring(2), "Mdm"))
                .AddTable("RegionalDirectorate", DbFluent.Table(nameof(DbRegionalDirectorate).Substring(2), "Mdm"));

            builder
                .AddRelationship("Employee_RegionalDirectorate", Table("Employee"), Table("RegionalDirectorate"),
                    (nameof(DbEmployee.EmployeeId), nameof(DbRegionalDirectorate.RegionalDirectorateId)));

            builder
                .AddTuple("Fields_SelectEmployee",
                    DbFluent.FieldAsAll(Table("Employee")),
                    DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId), Table("RegionalDirectorate")),
                    DbFluent.Field(nameof(DbRegionalDirectorate.Code), Table("RegionalDirectorate"))
                );

            builder
                .AddQuery(
                    new DbStoredQuery(
                    DbFluent.SelectQuery(Table("Employee"))
                    .From(
                        Table("Employee"),
                        DbFluent.Table(DbQueryJoinKind.Left, Table("RegionalDirectorate"))
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .AddIdField(p => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), p.UseParameter("code")))));
        }
    }
}

using BindOpen.Data.Common;
using BindOpen.Data.Elements;
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
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode1(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode1", p =>
                DbFluent.SelectQuery(Table<DbEmployee>())
                    .From(
                        Table<DbEmployee>(),
                        DbFluent.TableAsJoin(
                            DbQueryJoinKind.Left,
                            Table("RegionalDirectorate"),
                            JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text))))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode2(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode2", p =>
                DbFluent.SelectQuery(Table<DbEmployee>())
                    .From(
                        DbFluent.TableAsJoin(
                            DbQueryJoinKind.Left,
                            Table("RegionalDirectorate"),
                            JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text)))
                    .Where(DbFluent.Eq(DbFluent.Field("field1"), DbFluent.Field("field2"))))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode3(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode3", p =>
                DbFluent.SelectQuery(null)
                    .From(
                        Table<DbEmployee>(),
                        DbFluent.TableAsJoin(
                            DbQueryJoinKind.Left,
                            Table("RegionalDirectorate"),
                            JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .OrderBy(
                        DbFluent.OrderBy(Field<DbEmployee>(p => p.Code, "employee")),
                        DbFluent.OrderBy(Field<DbEmployee>(p => p.DateTimeField, "regionalDirectorate"), DataSortingModes.Descending))
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text))))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode4(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode4", p =>
                p.SelectQuery<DbEmployee>()
                    .From(
                        DbFluent.TableAsJoin(
                            DbQueryJoinKind.Left,
                            DbFluent.Table("RegionalDirectorate").WithAlias("directorate"),
                            JoinCondition("Employee_RegionalDirectorate", null, "directorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text)))
                    .WithCTE(
                        DbFluent.TableAsQuery(
                            DbFluent.SelectQuery(Table("RegionalDirectorate"))).WithAlias("directorate")
                    ))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode5(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode5", p =>
                p.SelectQuery<DbEmployee>()
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .Where(q => DbFluent.Exists(
                        q.UseSubQuery(
                            DbFluent.SelectQuery(DbFluent.Table("Employee", "Mdm"))
                            .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text))))))
                )
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode6(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode6", p =>
                p.SelectQuery<DbEmployee>()
                    .WithFields(Tuple("Fields_SelectEmployee2"))
                    .AddField(DbFluent.FieldAsScript<DbEmployee>(p => p.DateTimeField, "$sqlGetCurrentDate()"))
                )
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode7(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode7", p =>
                p.SelectQuery<DbEmployee>()
                    .WithFields(Tuple("Fields_SelectEmployee2"))
                    .AddField(DbFluent.FieldAsScript<DbEmployee>(p => p.DateTimeField, "$sqlGetCurrentDate()"))
                )
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }
    }
}

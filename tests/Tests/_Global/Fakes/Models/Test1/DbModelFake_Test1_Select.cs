using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Elements;
using BindOpen.Databases.Models;
using BindOpen.Databases.Data;
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
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode1(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode1", p =>
                BdoDb.SelectQuery(Table<DbEmployeeFake>())
                    .From(
                        Table<DbEmployeeFake>(),
                        BdoDb.TableAsJoin(
                            DbQueryJoinKind.Left,
                            Table("RegionalDirectorate"),
                            JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text))))
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode2(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode2", p =>
                BdoDb.SelectQuery(Table<DbEmployeeFake>())
                    .From(
                        BdoDb.TableAsJoin(
                            DbQueryJoinKind.Left,
                            Table("RegionalDirectorate"),
                            JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text)))
                    .Where(BdoDb.Eq(BdoDb.Field("field1"), BdoDb.Field("field2"))))
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode3(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode3", p =>
                BdoDb.SelectQuery(null)
                    .From(
                        Table<DbEmployeeFake>(),
                        BdoDb.TableAsJoin(
                            DbQueryJoinKind.Left,
                            Table("RegionalDirectorate"),
                            JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .OrderBy(
                        BdoDb.OrderBy(Field<DbEmployeeFake>(p => p.Code, "employee")),
                        BdoDb.OrderBy(Field<DbEmployeeFake>(p => p.DateTimeField, "regionalDirectorate"), DataSortingModes.Descending))
                    .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text))))
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode4(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode4", p =>
                p.SelectQuery<DbEmployeeFake>()
                    .From(
                        BdoDb.TableAsJoin(
                            DbQueryJoinKind.Left,
                            BdoDb.Table("RegionalDirectorate").WithAlias("directorate"),
                            JoinCondition("Employee_RegionalDirectorate", null, "directorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text)))
                    .WithCTE(
                        BdoDb.TableAsQuery(
                            BdoDb.SelectQuery(Table("RegionalDirectorate"))).WithAlias("directorate")
                    ))
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode5(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode5", p =>
                p.SelectQuery<DbEmployeeFake>()
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .Where(q => BdoDb.Exists(
                        q.UseSubQuery(
                            BdoDb.SelectQuery(BdoDb.Table("Employee", "Mdm"))
                            .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text))))))
                )
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode6(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode6", p =>
                p.SelectQuery<DbEmployeeFake>()
                    .WithFields(Tuple("Fields_SelectEmployee2"))
                    .AddField(BdoDb.FieldAsScript<DbEmployeeFake>(p => p.DateTimeField, "$sqlGetCurrentDate()"))
                    .OrderBy(
                        BdoDb.OrderBy(
                            BdoDb.FieldAsScript<DbEmployeeFake>(p => p.Code,
                                BdoDb.IfNull(
                                    BdoDb.Field<DbEmployeeFake>(p => p.Code),
                                    BdoDb.UpperCase(BdoDb.Value("TOTO"))))))
                )
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery SelectEmployeeWithCode7(string code)
        {
            return this.UseQuery("SelectEmployeeWithCode7", p =>
                p.SelectQuery<DbEmployeeFake>()
                    .WithFields(Tuple("Fields_SelectEmployee2"))
                    .AddField(BdoDb.FieldAsScript<DbEmployeeFake>(p => p.DateTimeField, "$sqlGetCurrentDate()"))
                )
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }
    }
}

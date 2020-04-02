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
                DbFluent.SelectQuery(Table("Employee"))
                    .From(
                        Table("Employee"),
                        DbFluent.TableAsJoin(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>())
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))))
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
                DbFluent.SelectQuery(Table("Employee"))
                    .From(
                        DbFluent.TableAsJoin(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>())
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))))
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
                        Table("Employee"),
                        DbFluent.TableAsJoin(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>())
                            .WithCondition(JoinCondition("Employee_RegionalDirectorate")))
                    .WithFields(Tuple("Fields_SelectEmployee"))
                    .WithLimit(100)
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }
    }
}

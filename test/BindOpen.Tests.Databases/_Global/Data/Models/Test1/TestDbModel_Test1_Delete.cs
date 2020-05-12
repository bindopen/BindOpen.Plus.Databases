using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
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
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee1(string code)
        {
            var query = DbFluent.DeleteQuery(Table<DbEmployee>())
                .AddIdField(DbFluent.FieldAsLiteral<DbEmployee>(p => p.Code, code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee2(string code)
        {
            var query = DbFluent.DeleteQuery(Table<DbEmployee>())
                .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text)))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee3(string code)
        {
            var query = DbFluent.DeleteQuery(Table<DbEmployee>())
                .Where(q => DataExpressionFactory.CreateAuto("{{" + string.Format("$sqlEq($sqlField('Code'), {0})", q.UseParameter("code", DataValueTypes.Text).AsScript()) + "}}"))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee4(string code)
        {
            var query = DbFluent.DeleteQuery(Table<DbEmployee>())
                .Where(q => DbFluent.Eq(DbFluent.Field("code"), q.UseParameter("code", DataValueTypes.Text).AsScript()))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee5(string code)
        {
            var query = DbFluent.DeleteQuery(Table<DbEmployee>())
                .Where(q => DbFluent.Eq(DbFluent.Field("code"), code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee6(string code)
        {
            var query = DbFluent.DeleteQuery(Table<DbEmployee>())
                .From(
                    DbFluent.TableAsJoin(
                        DbQueryJoinKind.Left,
                        Table("RegionalDirectorate").WithAlias("directorate"),
                        JoinCondition("Employee_RegionalDirectorate", null, "directorate")))
                .Where(q => DbFluent.Eq(DbFluent.Field("code"), code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee7(string code)
        {
            var query = DbFluent.DeleteQuery(Table<DbEmployee>())
                .From(
                    TableAsJoin<DbContact>(
                        DbQueryJoinKind.Left,
                        JoinCondition<DbEmployee, DbContact>())
                        .WithAlias("mainCountry"))
                .Where(q => DataExpressionFactory.CreateAuto("{{" + string.Format("$sqlEq($sqlField('Code'), {0})", q.UseParameter("code", DataValueTypes.Text).AsScript()) + "}}"))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee8(string code)
        {
            var query = DeleteQuery<DbEmployee>()
                .From(
                    DbFluent.TableAsJoin(DbQueryJoinKind.Left,
                        Table("RegionalDirectorate").WithAlias("directorate"),
                        JoinCondition("Employee_RegionalDirectorate")),
                    TableAsJoin<DbContact>(DbQueryJoinKind.Left,
                        JoinCondition<DbEmployee, DbContact>())
                        .WithAlias("mainCountry"),
                    DbFluent.TableAsJoin(
                        DbQueryJoinKind.Left,
                        Table<DbContact>().WithAlias("secondaryCountry"),
                        JoinCondition<DbEmployee, DbContact>("secondary")))
                .Where(q => DataExpressionFactory.CreateLiteral(@"""Code""='codeC'"))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));

            return query;
        }
    }
}

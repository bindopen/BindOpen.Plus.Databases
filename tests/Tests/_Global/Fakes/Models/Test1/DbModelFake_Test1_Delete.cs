using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Elements;
using BindOpen.Framework.MetaData.Expression;
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
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee1(string code)
        {
            var query = BdoDb.DeleteQuery(Table<DbEmployeeFake>())
                .AddIdField(BdoDb.FieldAsLiteral<DbEmployeeFake>(p => p.Code, code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee2(string code)
        {
            var query = BdoDb.DeleteQuery(Table<DbEmployeeFake>())
                .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text)))
                .WithParameters(
                    BdoData.NewScalar("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee3(string code)
        {
            var query = BdoDb.DeleteQuery(Table<DbEmployeeFake>())
                .Where(q => DataExpressionFactory.CreateExpAsAuto("{{" + string.Format("$sqlEq($sqlField('Code'), {0})", q.UseParameter("code", DataValueTypes.Text).AsExp()) + "}}"))
                .WithParameters(
                    BdoData.NewScalar("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee4(string code)
        {
            var query = BdoDb.DeleteQuery(Table<DbEmployeeFake>())
                .Where(q => BdoDb.Eq(BdoDb.Field("code"), q.UseParameter("code", DataValueTypes.Text)))
                .WithParameters(
                    BdoData.NewScalar("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee5(string code)
        {
            var query = BdoDb.DeleteQuery(Table<DbEmployeeFake>())
                .Where(q => BdoDb.Eq(BdoDb.Field("code"), code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee6(string code)
        {
            var query = BdoDb.DeleteQuery(Table<DbEmployeeFake>())
                .From(
                    BdoDb.TableAsJoin(
                        DbQueryJoinKind.Left,
                        Table("RegionalDirectorate").WithAlias("directorate"),
                        JoinCondition("Employee_RegionalDirectorate", null, "directorate")))
                .Where(q => BdoDb.Eq(BdoDb.Field("code"), code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee7(string code)
        {
            var query = BdoDb.DeleteQuery(Table<DbEmployeeFake>())
                .From(
                    TableAsJoin<DbContactFake>(
                        DbQueryJoinKind.Left,
                        JoinCondition<DbEmployeeFake, DbContactFake>())
                        .WithAlias("mainCountry"))
                .Where(q => DataExpressionFactory.CreateExpAsAuto("{{" + string.Format("$sqlEq($sqlField('Code'), {0})", q.UseParameter("code", DataValueTypes.Text).AsExp()) + "}}"))
                .WithParameters(
                    BdoData.NewScalar("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee8(string code)
        {
            var query = DeleteQuery<DbEmployeeFake>()
                .From(
                    BdoDb.TableAsJoin(DbQueryJoinKind.Left,
                        Table("RegionalDirectorate").WithAlias("directorate"),
                        JoinCondition("Employee_RegionalDirectorate")),
                    TableAsJoin<DbContactFake>(DbQueryJoinKind.Left,
                        JoinCondition<DbEmployeeFake, DbContactFake>())
                        .WithAlias("mainCountry"),
                    BdoDb.TableAsJoin(
                        DbQueryJoinKind.Left,
                        Table<DbContactFake>().WithAlias("secondaryCountry"),
                        JoinCondition<DbEmployeeFake, DbContactFake>("secondary")))
                .Where(q => DataExpressionFactory.CreateExpAsLiteral(@"""Code""='codeC'"))
                .WithParameters(
                    BdoData.NewScalar("code", code));

            return query;
        }

        /// <summary>
        /// Delete the specified employee.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery DeleteEmployee9(string code)
        {
            var query = BdoDb.DeleteQuery(Table<DbEmployeeFake>())
                .From(
                    BdoDb.TableAsJoin(DbQueryJoinKind.Left,
                        Table("RegionalDirectorate").WithAlias("directorate"),
                        JoinCondition("Employee_RegionalDirectorate")))
                .Where(q => BdoDb.Eq(BdoDb.Field("code"), q.UseParameter("code", DataValueTypes.Text)))
                .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text)))
                .WithParameters(
                    BdoData.NewScalar("code", code));

            return query;
        }
    }
}

using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Databases.Data.Models;
using BindOpen.Databases.Data.Queries;
using BindOpen.Tests.Databases.PostgreSql.Data.Dtos.Test1;
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
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpsertEmployee(EmployeeDto employee)
        {
            return DbFluent.Upsert(Table<DbEmployee>())
                .WithQueries(
                    DbFluent.UpdateQuery(Table<DbEmployee>())
                    .WithFields(q => new[]
                    {
                        DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueType.Date))
                    }),
                    DbFluent.InsertQuery(Table<DbEmployee>())
                        .WithFields(q => new[]
                        {
                            DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueType.Date))
                        })
                        .WithParameters(
                            ElementFactory.CreateScalar("code", employee.Code),
                            ElementFactory.CreateScalar("ByteArrayField", employee.ByteArrayField),
                            ElementFactory.CreateScalar("DoubleField", employee.DoubleField),
                            ElementFactory.CreateScalar("DateTimeField", employee.DateTimeField),
                            ElementFactory.CreateScalar("LongField", employee.LongField)))
                .WithCTE(
                    DbFluent.TableAsQuery(
                        DbFluent.SelectQuery(null)
                            .WithFields(q => new[]
                            {
                                DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))
                            }))
                    .WithAlias("T"))
                .WithParameters(
                    ElementFactory.CreateScalar("code", employee.Code),
                    ElementFactory.CreateScalar("ByteArrayField", employee.ByteArrayField),
                    ElementFactory.CreateScalar("DoubleField", employee.DoubleField),
                    ElementFactory.CreateScalar("DateTimeField", employee.DateTimeField),
                    ElementFactory.CreateScalar("LongField", employee.LongField));
        }
    }
}

using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Elements;
using BindOpen.Databases.Models;
using BindOpen.Databases.Data;
using BindOpen.Databases.Tests.PostgreSql.Data.Dtos.Test1;
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
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpsertEmployee(EmployeeDto employee)
        {
            return DbFluent.Upsert(Table<DbEmployee>())
                .WithQueries(
                    DbFluent.UpdateQuery(Table<DbEmployee>())
                    .WithFields(q => new[]
                    {
                        DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                    }),
                    DbFluent.InsertQuery(Table<DbEmployee>())
                        .WithFields(q => new[]
                        {
                            DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                        })
                        .WithParameters(
                            BdoElements.CreateScalar("code", employee.Code),
                            BdoElements.CreateScalar("ByteArrayField", employee.ByteArrayField),
                            BdoElements.CreateScalar("DoubleField", employee.DoubleField),
                            BdoElements.CreateScalar("DateTimeField", employee.DateTimeField),
                            BdoElements.CreateScalar("LongField", employee.LongField)))
                .WithCTE(
                    DbFluent.TableAsQuery(
                        DbFluent.SelectQuery(null)
                            .WithFields(q => new[]
                            {
                                DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text))
                            }))
                    .WithAlias("T"))
                .WithParameters(
                    BdoElements.CreateScalar("code", employee.Code),
                    BdoElements.CreateScalar("ByteArrayField", employee.ByteArrayField),
                    BdoElements.CreateScalar("DoubleField", employee.DoubleField),
                    BdoElements.CreateScalar("DateTimeField", employee.DateTimeField),
                    BdoElements.CreateScalar("LongField", employee.LongField));
        }
    }
}

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
        internal IDbQuery InsertEmployee1(EmployeeDto employee)
        {
            return DbFluent.InsertQuery(Table<DbEmployee>())
                .WithFields(q => new[]
                {
                    DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                })
                .WithReturnedIdFields(new[]
                {
                    Field<DbEmployee>(p=> p.EmployeeId)
                })
                .WithParameters(
                    BdoElements.CreateScalar("code", employee.Code),
                    BdoElements.CreateScalar("ByteArrayField", employee.ByteArrayField),
                    BdoElements.CreateScalar("DoubleField", employee.DoubleField),
                    BdoElements.CreateScalar("DateTimeField", employee.DateTimeField),
                    BdoElements.CreateScalar("LongField", employee.LongField));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery InsertEmployee2(EmployeeDto employee)
        {
            return DbFluent.InsertQuery(Table<DbEmployee>())
                .WithFields(q => new[]
                {
                    DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("newCode", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                })
                .WithIdFields(q => new[]
                {
                    DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("oldCode", DataValueTypes.Text))
                })
                .WithReturnedIdFields(new[]
                {
                    Field<DbEmployee>(p=> p.EmployeeId)
                })
                .WithParameters(
                    BdoElements.CreateScalar("newCode", employee.Code),
                    BdoElements.CreateScalar("oldCode", "oldCode"),
                    BdoElements.CreateScalar("ByteArrayField", employee.ByteArrayField),
                    BdoElements.CreateScalar("DoubleField", employee.DoubleField),
                    BdoElements.CreateScalar("DateTimeField", employee.DateTimeField),
                    BdoElements.CreateScalar("LongField", employee.LongField));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery InsertEmployee3(EmployeeDto employee)
        {
            return DbFluent.InsertQuery(Table<DbEmployee>())
                .WithFields(q => new[]
                {
                    DbFluent.Field(nameof(DbEmployee.Code)),
                    DbFluent.Field(nameof(DbEmployee.ByteArrayField)),
                    DbFluent.Field(nameof(DbEmployee.DoubleField)),
                    DbFluent.Field(nameof(DbEmployee.DateTimeField)),
                    DbFluent.Field(nameof(DbEmployee.LongField))
                })
                .WithReturnedIdFields(new[]
                {
                    Field<DbEmployee>(p=> p.EmployeeId)
                })
                .From(p => p.UseSubQuery(
                    DbFluent.SelectQuery(Table<DbEmployee>())
                        .WithFields(q => new[]
                        {
                            DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("newCode", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("LongField", DataValueTypes.Date)),

                            DbFluent.FieldAsQuery<DbContact>(p => p.ContactId,
                                DbFluent.SelectQuery(Table<DbContact>())
                                    .WithLimit(1)
                                    .AddField(Field<DbContact>(f=>f.ContactId))
                                    .AddIdField(
                                        DbFluent.FieldAsParameter<DbContact>(f => f.Code, q.UseParameter("contactCode", DataValueTypes.Text)))),
                        })
                        .WithIdFields(q => new[]
                        {
                            DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("oldCode", DataValueTypes.Text))
                        })
                        .WithParameters(
                            BdoElements.CreateScalar("newCode", employee.Code),
                            BdoElements.CreateScalar("contactCode", "contactCodeA"),
                            BdoElements.CreateScalar("oldCode", "oldCode"),
                            BdoElements.CreateScalar("ByteArrayField", employee.ByteArrayField),
                            BdoElements.CreateScalar("DoubleField", employee.DoubleField),
                            BdoElements.CreateScalar("DateTimeField", employee.DateTimeField),
                            BdoElements.CreateScalar("LongField", employee.LongField))));
        }
    }
}

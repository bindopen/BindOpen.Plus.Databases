using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Elements;
using BindOpen.Databases.Models;
using BindOpen.Databases.Data;
using BindOpen.Databases.Tests.PostgreSql.Data.Dtos.Test1;
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
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery InsertEmployee1(EmployeeDtoFake employee)
        {
            return BdoDb.InsertQuery(Table<DbEmployeeFake>())
                .WithFields(q => new[]
                {
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text)),
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                })
                .WithReturnedIdFields(new[]
                {
                    Field<DbEmployeeFake>(p=> p.EmployeeId)
                })
                .WithParameters(
                    BdoData.NewScalar("code", employee.Code),
                    BdoData.NewScalar("ByteArrayField", employee.ByteArrayField),
                    BdoData.NewScalar("DoubleField", employee.DoubleField),
                    BdoData.NewScalar("DateTimeField", employee.DateTimeField),
                    BdoData.NewScalar("LongField", employee.LongField));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery InsertEmployee2(EmployeeDtoFake employee)
        {
            return BdoDb.InsertQuery(Table<DbEmployeeFake>())
                .WithFields(q => new[]
                {
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("newCode", DataValueTypes.Text)),
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                })
                .WithIdFields(q => new[]
                {
                    BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("oldCode", DataValueTypes.Text))
                })
                .WithReturnedIdFields(new[]
                {
                    Field<DbEmployeeFake>(p=> p.EmployeeId)
                })
                .WithParameters(
                    BdoData.NewScalar("newCode", employee.Code),
                    BdoData.NewScalar("oldCode", "oldCode"),
                    BdoData.NewScalar("ByteArrayField", employee.ByteArrayField),
                    BdoData.NewScalar("DoubleField", employee.DoubleField),
                    BdoData.NewScalar("DateTimeField", employee.DateTimeField),
                    BdoData.NewScalar("LongField", employee.LongField));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery InsertEmployee3(EmployeeDtoFake employee)
        {
            return BdoDb.InsertQuery(Table<DbEmployeeFake>())
                .WithFields(q => new[]
                {
                    BdoDb.Field(nameof(DbEmployeeFake.Code)),
                    BdoDb.Field(nameof(DbEmployeeFake.ByteArrayField)),
                    BdoDb.Field(nameof(DbEmployeeFake.DoubleField)),
                    BdoDb.Field(nameof(DbEmployeeFake.DateTimeField)),
                    BdoDb.Field(nameof(DbEmployeeFake.LongField))
                })
                .WithReturnedIdFields(new[]
                {
                    Field<DbEmployeeFake>(p=> p.EmployeeId)
                })
                .From(p => p.UseSubQuery(
                    BdoDb.SelectQuery(Table<DbEmployeeFake>())
                        .WithFields(q => new[]
                        {
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("newCode", DataValueTypes.Text)),
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.LongField), q.UseParameter("LongField", DataValueTypes.Date)),

                            BdoDb.FieldAsQuery<DbContactFake>(p => p.ContactId,
                                BdoDb.SelectQuery(Table<DbContactFake>())
                                    .WithLimit(1)
                                    .AddField(Field<DbContactFake>(f=>f.ContactId))
                                    .AddIdField(
                                        BdoDb.FieldAsParameter<DbContactFake>(f => f.Code, q.UseParameter("contactCode", DataValueTypes.Text)))),
                        })
                        .WithIdFields(q => new[]
                        {
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("oldCode", DataValueTypes.Text))
                        })
                        .WithParameters(
                            BdoData.NewScalar("newCode", employee.Code),
                            BdoData.NewScalar("contactCode", "contactCodeA"),
                            BdoData.NewScalar("oldCode", "oldCode"),
                            BdoData.NewScalar("ByteArrayField", employee.ByteArrayField),
                            BdoData.NewScalar("DoubleField", employee.DoubleField),
                            BdoData.NewScalar("DateTimeField", employee.DateTimeField),
                            BdoData.NewScalar("LongField", employee.LongField))));
        }
    }
}

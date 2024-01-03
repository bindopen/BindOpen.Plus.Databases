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
        internal IDbQuery UpsertEmployee(EmployeeDtoFake employee)
        {
            return BdoDb.Upsert(Table<DbEmployeeFake>())
                .WithQueries(
                    BdoDb.UpdateQuery(Table<DbEmployeeFake>())
                    .WithFields(q => new[]
                    {
                        BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text)),
                        BdoDb.FieldAsParameter(nameof(DbEmployeeFake.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                        BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                        BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                        BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                        BdoDb.FieldAsParameter(nameof(DbEmployeeFake.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                    }),
                    BdoDb.InsertQuery(Table<DbEmployeeFake>())
                        .WithFields(q => new[]
                        {
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text)),
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.ByteArrayField), q.UseParameter("ByteArrayField", DataValueTypes.Text)),
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DoubleField), q.UseParameter("DoubleField", DataValueTypes.Text)),
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DateTimeField), q.UseParameter("DateTimeField", DataValueTypes.Text)),
                            BdoDb.FieldAsParameter(nameof(DbEmployeeFake.LongField), q.UseParameter("LongField", DataValueTypes.Date))
                        })
                        .WithParameters(
                            BdoData.NewScalar("code", employee.Code),
                            BdoData.NewScalar("ByteArrayField", employee.ByteArrayField),
                            BdoData.NewScalar("DoubleField", employee.DoubleField),
                            BdoData.NewScalar("DateTimeField", employee.DateTimeField),
                            BdoData.NewScalar("LongField", employee.LongField)))
                .WithCTE(
                    BdoDb.TableAsQuery(
                        BdoDb.SelectQuery(null)
                            .WithFields(q => new[]
                            {
                                BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text))
                            }))
                    .WithAlias("T"))
                .WithParameters(
                    BdoData.NewScalar("code", employee.Code),
                    BdoData.NewScalar("ByteArrayField", employee.ByteArrayField),
                    BdoData.NewScalar("DoubleField", employee.DoubleField),
                    BdoData.NewScalar("DateTimeField", employee.DateTimeField),
                    BdoData.NewScalar("LongField", employee.LongField));
        }
    }
}

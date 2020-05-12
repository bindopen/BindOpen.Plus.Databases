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
                    ElementFactory.CreateScalar("code", employee.Code),
                    ElementFactory.CreateScalar("ByteArrayField", employee.ByteArrayField),
                    ElementFactory.CreateScalar("DoubleField", employee.DoubleField),
                    ElementFactory.CreateScalar("DateTimeField", employee.DateTimeField),
                    ElementFactory.CreateScalar("LongField", employee.LongField));
        }
    }
}

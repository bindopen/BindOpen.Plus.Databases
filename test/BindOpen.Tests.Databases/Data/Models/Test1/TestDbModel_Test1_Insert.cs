using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Databases.Data.Models;
using BindOpen.Databases.Data.Queries;
using BindOpen.Tests.Databases.Data.Dtos.Test1;
using BindOpen.Tests.Databases.Data.Entities.Test1;

namespace BindOpen.Tests.Databases.Data.Models
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
            return DbFluent.InsertQuery(Table("Employee"))
                .WithFields(q => new[]
                {
                    DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.ContactEmail), q.UseParameter("contactEmail", DataValueType.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.LastName), q.UseParameter("lastName", DataValueType.Text)),
                    DbFluent.FieldAsParameter(nameof(DbEmployee.StaffNumber), q.UseParameter("staffNumber", DataValueType.Text))
                })
                .WithReturnedIdFields(new[]
                {
                    Field(nameof(DbEmployee.EmployeeId), "Employee")
                })
                .WithParameters(
                    ElementFactory.Create("code", employee.Code),
                    ElementFactory.Create("contactEmail", employee.ContactEmail),
                    ElementFactory.Create("fisrtName", employee.FisrtName),
                    ElementFactory.Create("lastName", employee.LastName),
                    ElementFactory.Create("staffNumber", employee.StaffNumber));
        }
    }
}

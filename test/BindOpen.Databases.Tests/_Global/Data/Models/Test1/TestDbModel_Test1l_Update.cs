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
        /// <param name="code"></param>
        /// <param name="isPartialUpdate"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpdateEmployee1(string code, bool isPartialUpdate, EmployeeDto employee)
        {
            return this.UseQuery("UpdateEmployee1",
                p =>
                {
                    var query = DbFluent.UpdateQuery(Table<DbEmployee>())
                        .From(
                            DbFluent.TableAsJoin(
                                DbQueryJoinKind.Left,
                                Table("RegionalDirectorate"),
                                JoinCondition("Employee_RegionalDirectorate")))
                        .WithReturnedIdFields(Field("Code", "DbEmployee"));

                    query.AddField(
                        !isPartialUpdate || employee?.Code?.Length > 0,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueTypes.Text)));

                    query.AddField(
                        !isPartialUpdate || employee?.ByteArrayField?.Length > 0,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.ByteArrayField), q.UseParameter("byteArrayField", DataValueTypes.ByteArray)));

                    query.AddField(
                        !isPartialUpdate || employee?.DateTimeField != null,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.DoubleField), q.UseParameter("doubleField", DataValueTypes.Number)));

                    query.AddField(
                        !isPartialUpdate || employee?.DoubleField != null,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.DateTimeField), q.UseParameter("dateTimeField", DataValueTypes.Date)));

                    query.AddField(
                        true,
                        q => DbFluent.FieldAsParameter(nameof(DbEmployee.LongField), q.UseParameter("longField", DataValueTypes.Long)));

                    query.AddField(
                        !isPartialUpdate || employee?.RegionalDirectorateCode?.Length > 0,
                        q => DbFluent.FieldAsQuery(nameof(DbEmployee.RegionalDirectorateId),
                                DbFluent.SelectQuery(Table("RegionalDirectorate"))
                                    .AddField(DbFluent.Field(nameof(DbRegionalDirectorate.RegionalDirectorateId)))
                                    .WithIdFields(
                                        DbFluent.FieldAsParameter(nameof(DbRegionalDirectorate.Code), q.UseParameter("directorateCode", DataValueTypes.Text)))));

                    return query;
                })
                .WithParameters(
                    BdoElements.CreateScalar("code", code),
                    BdoElements.CreateScalar("byteArrayField", employee.ByteArrayField),
                    BdoElements.CreateScalar("doubleField", employee.DoubleField),
                    BdoElements.CreateScalar("dateTimeField", employee.DateTimeField),
                    BdoElements.CreateScalar("directorateCode", null),
                    BdoElements.CreateScalar("longField", employee.LongField));
        }

        /// <summary>
        /// Updates the regional directorate of the specified Fidal employee.
        /// </summary>
        /// <param name="regionalDirectorateCode">The code of the regional directorate to consider.</param>
        /// <returns>Returns the query.</returns>
        internal IDbQuery UpdateEmployee2(string regionalDirectorateCode)
        {
            return this.UseQuery("UpdateEmployee2", p =>
                DbFluent.UpdateQuery(Table("DbEmployee", "employee"))
                    .AddField(q => DbFluent.Field<DbEmployee>(q => q.RegionalDirectorateId).AsNull())
                    .From(Table("RegionalDirectorate", "regionalDirectorate"))
                    .AddIdField(q =>
                        DbFluent.FieldAsOther<DbEmployee>(
                            t => t.RegionalDirectorateId, Table("DbEmployee", "employee"),
                            DbFluent.Field<DbRegionalDirectorate>(
                                t => t.RegionalDirectorateId, Table("RegionalDirectorate").WithAlias("regionalDirectorate"))))
                    .AddIdField(q =>
                        DbFluent.FieldAsParameter<DbEmployee>(
                            t => t.Code,
                            DbFluent.Table("employee"),
                            q.UseParameter("regionalDirectorateCode", DataValueTypes.Text)))
                    .WithReturnedIdFields(Field("Code", "DbEmployee", "employee"))
                )
                .WithParameters(
                    BdoElements.CreateScalar("regionalDirectorateCode", regionalDirectorateCode));
        }

        /// <summary>
        /// Updates the regional directorate of the specified Fidal employee.
        /// </summary>
        /// <param name="regionalDirectorateCode">The code of the regional directorate to consider.</param>
        /// <returns>Returns the query.</returns>
        internal IDbQuery UpdateEmployee3(string regionalDirectorateCode)
        {
            return this.UseQuery("UpdateFidalEmployee_RegionalDirectorateAsNull", p =>
                DbFluent.UpdateQuery(Table("DbEmployee", "employee"))
                    .AddField(q => DbFluent.Field<DbEmployee>(q => q.RegionalDirectorateId).AsNull())
                    .AddField(q => DbFluent.FieldAsParameter<DbEmployee>(q => q.LongField, BdoElements.CreateScalar("length", DataValueTypes.Long)))
                    .From(Table("RegionalDirectorate", "regionalDirectorate"))
                    .AddIdField(q =>
                        DbFluent.FieldAsOther<DbEmployee>(
                            t => t.RegionalDirectorateId, Table("DbEmployee", "employee"),
                            DbFluent.Field<DbRegionalDirectorate>(
                                t => t.RegionalDirectorateId, Table("RegionalDirectorate").WithAlias("regionalDirectorate"))))
                    .AddIdField(q =>
                        DbFluent.FieldAsParameter<DbEmployee>(
                            t => t.Code,
                            DbFluent.Table("employee"),
                            q.UseParameter("regionalDirectorateCode", DataValueTypes.Text)))
                    .WithReturnedIdFields(Field("Code", "DbEmployee", "employee"))
                )
                .WithParameters(
                    BdoElements.CreateScalar("regionalDirectorateCode", regionalDirectorateCode),
                    BdoElements.CreateScalar("length", 2500));
        }

        /// <summary>
        /// Updates the regional directorate of the specified Fidal employee.
        /// </summary>
        /// <param name="regionalDirectorateCode">The code of the regional directorate to consider.</param>
        /// <returns>Returns the query.</returns>
        internal IDbQuery UpdateEmployee4(string regionalDirectorateCode)
        {
            return this.UseQuery("UpdateFidalEmployee_Simple", p =>
                DbFluent.UpdateQuery(Table("DbEmployee", "employee"))
                    .AddField(q => DbFluent.Field<DbEmployee>(q => q.RegionalDirectorateId).AsNull())
                    .AddField(q => DbFluent.FieldAsParameter<DbEmployee>(q => q.LongField, BdoElements.CreateScalar("length", DataValueTypes.Long)))
                    .AddIdField(q =>
                        DbFluent.FieldAsOther<DbEmployee>(
                            t => t.RegionalDirectorateId, Table("DbEmployee", "employee"),
                            DbFluent.Field<DbRegionalDirectorate>(
                                t => t.RegionalDirectorateId, Table("RegionalDirectorate").WithAlias("regionalDirectorate"))))
                    .AddIdField(q =>
                        DbFluent.FieldAsParameter<DbEmployee>(
                            t => t.Code,
                            DbFluent.Table("employee"),
                            q.UseParameter("regionalDirectorateCode", DataValueTypes.Text)))
                    .WithReturnedIdFields(Field("Code", "DbEmployee", "employee"))
                )
                .WithParameters(
                    BdoElements.CreateScalar("regionalDirectorateCode", regionalDirectorateCode),
                    BdoElements.CreateScalar("length", 2500));
        }
    }
}

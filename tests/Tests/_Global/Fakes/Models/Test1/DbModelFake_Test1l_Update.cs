using BindOpen.Data;
using BindOpen.Databases.Relational;
using BindOpen.Databases.Tests.Fakes.Test1;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a test database model.
    /// </summary>
    public partial class DbModelFake : BdoDbRelationalModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isPartialUpdate"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpdateEmployee1(string code, bool isPartialUpdate, EmployeeDtoFake employee)
        {
            return this.UseQuery("UpdateEmployee1",
                p =>
                {
                    var query = BdoDb.UpdateQuery(Table<DbEmployeeFake>())
                        .From(
                            BdoDb.TableAsJoin(
                                DbQueryJoinKind.Left,
                                Table("RegionalDirectorate"),
                                JoinCondition("Employee_RegionalDirectorate")))
                        .WithReturnedIdFields(Field("Code", "DbEmployee"));

                    query.AddField(
                        !isPartialUpdate || employee?.Code?.Length > 0,
                        q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.Code), q.UseParameter("code", DataValueTypes.Text)));

                    query.AddField(
                        !isPartialUpdate || employee?.ByteArrayField?.Length > 0,
                        q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.ByteArrayField), q.UseParameter("byteArrayField", DataValueTypes.Binary)));

                    query.AddField(
                        !isPartialUpdate || employee?.DateTimeField != null,
                        q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DoubleField), q.UseParameter("doubleField", DataValueTypes.Number)));

                    query.AddField(
                        !isPartialUpdate || employee?.DoubleField != null,
                        q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.DateTimeField), q.UseParameter("dateTimeField", DataValueTypes.Date)));

                    query.AddField(
                        true,
                        q => BdoDb.FieldAsParameter(nameof(DbEmployeeFake.LongField), q.UseParameter("longField", DataValueTypes.Long)));

                    query.AddField(
                        !isPartialUpdate || employee?.RegionalDirectorateCode?.Length > 0,
                        q => BdoDb.FieldAsQuery(nameof(DbEmployeeFake.RegionalDirectorateId),
                                BdoDb.SelectQuery(Table("RegionalDirectorate"))
                                    .AddField(BdoDb.Field(nameof(DbRegionalDirectorateFake.RegionalDirectorateId)))
                                    .WithIdFields(
                                        BdoDb.FieldAsParameter(nameof(DbRegionalDirectorateFake.Code), q.UseParameter("directorateCode", DataValueTypes.Text)))));

                    return query;
                })
                .WithParameters(
                    BdoData.NewScalar("code", code),
                    BdoData.NewScalar("byteArrayField", employee.ByteArrayField),
                    BdoData.NewScalar("doubleField", employee.DoubleField),
                    BdoData.NewScalar("dateTimeField", employee.DateTimeField),
                    BdoData.NewScalar("directorateCode", null),
                    BdoData.NewScalar("longField", employee.LongField));
        }

        /// <summary>
        /// Updates the regional directorate of the specified Fidal employee.
        /// </summary>
        /// <param name="regionalDirectorateCode">The code of the regional directorate to consider.</param>
        /// <returns>Returns the query.</returns>
        internal IDbQuery UpdateEmployee2(string regionalDirectorateCode)
        {
            return this.UseQuery("UpdateEmployee2", p =>
                BdoDb.UpdateQuery(Table("DbEmployee", "employee"))
                    .AddField(q => BdoDb.Field<DbEmployeeFake>(q => q.RegionalDirectorateId).AsNull())
                    .From(Table("RegionalDirectorate", "regionalDirectorate"))
                    .AddIdField(q =>
                        BdoDb.FieldAsOther<DbEmployeeFake>(
                            t => t.RegionalDirectorateId, Table("DbEmployee", "employee"),
                            BdoDb.Field<DbRegionalDirectorateFake>(
                                t => t.RegionalDirectorateId, Table("RegionalDirectorate").WithAlias("regionalDirectorate"))))
                    .AddIdField(q =>
                        BdoDb.FieldAsParameter<DbEmployeeFake>(
                            t => t.Code,
                            BdoDb.Table("employee"),
                            q.UseParameter("regionalDirectorateCode", DataValueTypes.Text)))
                    .WithReturnedIdFields(Field("Code", "DbEmployee", "employee"))
                )
                .WithParameters(
                    BdoData.NewScalar("regionalDirectorateCode", regionalDirectorateCode));
        }

        /// <summary>
        /// Updates the regional directorate of the specified Fidal employee.
        /// </summary>
        /// <param name="regionalDirectorateCode">The code of the regional directorate to consider.</param>
        /// <returns>Returns the query.</returns>
        internal IDbQuery UpdateEmployee3(string regionalDirectorateCode)
        {
            return this.UseQuery("UpdateFidalEmployee_RegionalDirectorateAsNull", p =>
                BdoDb.UpdateQuery(Table("DbEmployee", "employee"))
                    .AddField(q => BdoDb.Field<DbEmployeeFake>(q => q.RegionalDirectorateId).AsNull())
                    .AddField(q => BdoDb.FieldAsParameter<DbEmployeeFake>(q => q.LongField, BdoData.NewScalar("length", DataValueTypes.Long)))
                    .From(Table("RegionalDirectorate", "regionalDirectorate"))
                    .AddIdField(q =>
                        BdoDb.FieldAsOther<DbEmployeeFake>(
                            t => t.RegionalDirectorateId, Table("DbEmployee", "employee"),
                            BdoDb.Field<DbRegionalDirectorateFake>(
                                t => t.RegionalDirectorateId, Table("RegionalDirectorate").WithAlias("regionalDirectorate"))))
                    .AddIdField(q =>
                        BdoDb.FieldAsParameter<DbEmployeeFake>(
                            t => t.Code,
                            BdoDb.Table("employee"),
                            q.UseParameter("regionalDirectorateCode", DataValueTypes.Text)))
                    .WithReturnedIdFields(Field("Code", "DbEmployee", "employee"))
                )
                .WithParameters(
                    BdoData.NewScalar("regionalDirectorateCode", regionalDirectorateCode),
                    BdoData.NewScalar("length", 2500));
        }

        /// <summary>
        /// Updates the regional directorate of the specified Fidal employee.
        /// </summary>
        /// <param name="regionalDirectorateCode">The code of the regional directorate to consider.</param>
        /// <returns>Returns the query.</returns>
        internal IDbQuery UpdateEmployee4(string regionalDirectorateCode)
        {
            return this.UseQuery("UpdateFidalEmployee_Simple", p =>
                BdoDb.UpdateQuery(Table("DbEmployee", "employee"))
                    .AddField(q => BdoDb.Field<DbEmployeeFake>(q => q.RegionalDirectorateId).AsNull())
                    .AddField(q => BdoDb.FieldAsParameter<DbEmployeeFake>(q => q.LongField, BdoData.NewScalar("length", DataValueTypes.Long)))
                    .AddIdField(q =>
                        BdoDb.FieldAsOther<DbEmployeeFake>(
                            t => t.RegionalDirectorateId, Table("DbEmployee", "employee"),
                            BdoDb.Field<DbRegionalDirectorateFake>(
                                t => t.RegionalDirectorateId, Table("RegionalDirectorate").WithAlias("regionalDirectorate"))))
                    .AddIdField(q =>
                        BdoDb.FieldAsParameter<DbEmployeeFake>(
                            t => t.Code,
                            BdoDb.Table("employee"),
                            q.UseParameter("regionalDirectorateCode", DataValueTypes.Text)))
                    .WithReturnedIdFields(Field("Code", "DbEmployee", "employee"))
                )
                .WithParameters(
                    BdoData.NewScalar("regionalDirectorateCode", regionalDirectorateCode),
                    BdoData.NewScalar("length", 2500));
        }
    }
}

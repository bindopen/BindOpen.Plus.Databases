using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Databases.Data.Models;
using BindOpen.Databases.Data.Queries;
using BindOpen.Tests.Databases.Data.Dtos.Test2;
using BindOpen.Tests.Databases.Data.Entities.Test1;
using BindOpen.Tests.Databases.Data.Entities.Test2;

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
        /// <param name="q"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageToken"></param>
        /// <returns></returns>
        internal IDbQuery QuerySelectCountries(string q, string orderBy, int? pageSize = null, string pageToken = null)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal IDbQuery QuerySelectCountryWithCode(string code)
        {
            return this.UseQuery("GetCountryWithCode", p =>
                DbFluent.SelectQuery(Table("Country"))
                    .From(
                        DbFluent.Table(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>(),
                            JoinCondition("Country_RegionalDirectorate")))
                    .WithFields(Tuple("SelectCountry"))
                    .AddIdField(DbFluent.FieldAsParameter(nameof(DbCountry.Code), "code"))
                    .UsingParameters(ElementSpecFactory.Create("code", DataValueType.Text))
                )
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        internal IDbQuery QueryInsertCountry(CountryDto country)
        {
            return DbFluent.InsertQuery(Table("Country"), true)
                .From(
                    DbFluent.Table(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>(),
                        JoinCondition("Country_RegionalDirectorate")));
            //.WithFields(p => Fields_InsertCountry(p, country));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isPartialUpdate"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        internal IDbQuery QueryUpdateCountry(string code, bool isPartialUpdate, CountryDto country)
        {
            var query = DbFluent.UpdateQuery(Table("Country"))
                .From(
                    DbFluent.Table(DbQueryJoinKind.Left, Table<DbRegionalDirectorate>(),
                        JoinCondition("Country_RegionalDirectorate")));

            if (!isPartialUpdate || country?.Code?.Length > 0)
            {
                query.AddField(p => DbFluent.FieldAsParameter(nameof(DbCountry.Code), p.UseParameter("code", nameof(DbCountry.Code))));
            }

            return query;
        }

        /// <summary>
        /// Delete the specified country.
        /// </summary>
        /// <param name="code">The code to consider.</param>
        /// <returns>Returns the generated query.</returns>
        internal IDbQuery QueryDeleteCountry(string code)
        {
            return this.UseQuery("DeleteCountry", p =>
                DbFluent.DeleteQuery(Table("Country"))
                    .AddIdField(q => DbFluent.FieldAsParameter(nameof(DbCountry.Code), q.UseParameter("code", DataValueType.Text))))
                .WithParameters(
                    ElementFactory.CreateScalar("code", code));
        }
    }
}

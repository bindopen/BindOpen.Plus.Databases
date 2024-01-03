using BindOpen.Data;
using BindOpen.Databases.Models;
using BindOpen.Databases.Tests.Fakes.Test2;

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
                BdoDb.SelectQuery(Table("Country"))
                    .From(
                        BdoDb.TableAsJoin(DbQueryJoinKind.Left, Table("RegionalDirectorate"),
                            JoinCondition("Country_RegionalDirectorate")))
                    .WithFields(Tuple("SelectCountry"))
                    .AddIdField(BdoDb.FieldAsParameter(nameof(DbCountryFake.Code), "code"))
                    .UsingParameters(BdoData.NewSpec("code", DataValueTypes.Text))
                )
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        internal IDbQuery QueryInsertCountry(CountryDtoFake country)
        {
            return BdoDb.InsertQuery(Table("Country"), true)
                .From(
                    BdoDb.TableAsJoin(DbQueryJoinKind.Left, Table("RegionalDirectorate"),
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
        internal IDbQuery QueryUpdateCountry(string code, bool isPartialUpdate, CountryDtoFake country)
        {
            var query = BdoDb.UpdateQuery(Table("Country"))
                .From(
                    BdoDb.TableAsJoin(DbQueryJoinKind.Left, Table("RegionalDirectorate"),
                        JoinCondition("Country_RegionalDirectorate")));

            if (!isPartialUpdate || country?.Code?.Length > 0)
            {
                query.AddField(p => BdoDb.FieldAsParameter(nameof(DbCountryFake.Code), p.UseParameter("code", nameof(DbCountryFake.Code))));
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
                BdoDb.DeleteQuery(Table("Country"))
                    .AddIdField(q => BdoDb.FieldAsParameter(nameof(DbCountryFake.Code), q.UseParameter("code", DataValueTypes.Text))))
                .WithParameters(
                    BdoData.NewScalar("code", code));
        }
    }
}

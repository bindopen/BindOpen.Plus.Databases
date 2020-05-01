using BindOpen.Databases.Data.Models;
using BindOpen.Databases.Data.Queries;
using BindOpen.Tests.Databases.PostgreSql.Data.Entities;
using BindOpen.Tests.Databases.PostgreSql.Data.Entities.Test2;

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
        public void OnCreating_Test2()
        {
            // Community

            AddTable<DbCommunity>(
                    DbFluent.Table(nameof(DbCommunity).Substring(2), DbSchemas.Test2.ToString()),
                    q => q.CommunityId,
                    q => q.Code,
                    q => q.CreationDate,
                    q => q.LabelEN,
                    q => q.LabelFR,
                    q => q.LastModificationDate)
                .AddTuple(
                    "SelectCommunity",
                    AllFields<DbCommunity>().ToArray()
                )
                .AddTuple(
                    "UpdateCommunity",
                    DbFluent.Field(nameof(DbCommunity.Code), Table<DbCommunity>()),
                    Field<DbCommunity>(q => q.LabelEN),
                    Field(nameof(DbCommunity.LabelFR), "DbCommunity"),
                    DbFluent.Field(nameof(DbCommunity.LastModificationDate), Table<DbCommunity>())
                );

            // Country

            AddTable<DbCountry>(
                    DbFluent.Table(nameof(DbCountry).Substring(2), DbSchemas.Test2.ToString()),
                    q => q.CountryId,
                    q => q.Code,
                    q => q.CreationDate,
                    q => q.LabelEN,
                    q => q.LabelFR,
                    q => q.LastModificationDate)
                .AddTuple(
                    "SelectCountry",
                    Field<DbCountry>(q => q.CountryId),
                    DbFluent.Field(nameof(DbCountry.Code), Table<DbCountry>()),
                    DbFluent.Field(nameof(DbCountry.CreationDate)),
                    DbFluent.Field(nameof(DbCountry.LabelEN)),
                    DbFluent.Field(nameof(DbCountry.LabelFR)),
                    DbFluent.Field(nameof(DbCountry.LastModificationDate)))
                .AddTuple(
                    "UpdateCountry",
                    DbFluent.Field(nameof(DbCountry.Code)),
                    DbFluent.Field(nameof(DbCountry.LabelEN)),
                    DbFluent.Field(nameof(DbCountry.LabelFR)),
                    DbFluent.Field(nameof(DbCountry.LastModificationDate)));

            // Country_Community

            AddTable("Country_Community", DbFluent.Table(nameof(DbCountry_Community).Substring(2), DbSchemas.Test2.ToString()))
                .AddTuple(
                    "SelectCountry_Community",
                    DbFluent.Field(nameof(DbCountry_Community.CountryId), Table("Country_Community")),
                    DbFluent.Field(nameof(DbCountry_Community.CommunityId), Table("Country_Community")))
                .AddTuple(
                    "UpdateCountry",
                    DbFluent.Field(nameof(DbCountry_Community.CountryId), Table("Country_Community")),
                    DbFluent.Field(nameof(DbCountry_Community.CommunityId), Table("Country_Community")));

            // CountryInformation

            AddTable<DbCountryInformation>(
                    DbFluent.Table(nameof(DbCountryInformation).Substring(2), DbSchemas.Test2.ToString()))
                .AddTuple(
                    "SelectCountryInformation",
                    DbFluent.Field(nameof(DbCountryInformation.CountryId)),
                    DbFluent.Field(nameof(DbCountryInformation.CreationDate)),
                    DbFluent.Field(nameof(DbCountryInformation.LastModificationDate)),
                    DbFluent.Field(nameof(DbCountryInformation.StartDate)))
                .AddTuple(
                    "UpdateCountryInformation",
                    DbFluent.Field(nameof(DbCountryInformation.LastModificationDate)),
                    DbFluent.Field(nameof(DbCountryInformation.StartDate)));

            // Relationships

            AddRelationship(
                    "Community_Country_Community",
                    Table<DbCommunity>(),
                    Table("Country_Community"),
                    (nameof(DbCommunity.CommunityId), nameof(DbCountry_Community.CommunityId)))
                .AddRelationship(
                    "Community_Country_Community",
                    Table<DbCountry>(),
                    Table("Country_Community"),
                    (nameof(DbCountry.CountryId), nameof(DbCountry_Community.CountryId)))
                .AddRelationship<DbCountryInformation, DbCountry>(
                    (q => q.CountryId, q => q.CountryId));
        }
    }
}

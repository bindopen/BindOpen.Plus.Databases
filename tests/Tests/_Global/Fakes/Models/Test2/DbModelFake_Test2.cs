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
        public void OnCreating_Test2()
        {
            // Community

            AddTable<DbCommunityFake>(
                    BdoDb.Table(nameof(DbCommunityFake).Substring(2), DbSchemas.Test2.ToString()),
                    q => q.CommunityId,
                    q => q.Code,
                    q => q.CreationDate,
                    q => q.LabelEN,
                    q => q.LabelFR,
                    q => q.LastModificationDate)
                .AddTuple(
                    "SelectCommunity",
                    AllFields<DbCommunityFake>().ToArray()
                )
                .AddTuple(
                    "UpdateCommunity",
                    BdoDb.Field(nameof(DbCommunityFake.Code), Table<DbCommunityFake>()),
                    Field<DbCommunityFake>(q => q.LabelEN),
                    Field(nameof(DbCommunityFake.LabelFR), "DbCommunity"),
                    BdoDb.Field(nameof(DbCommunityFake.LastModificationDate), Table<DbCommunityFake>())
                );

            // Country

            AddTable<DbCountryFake>(
                    BdoDb.Table(nameof(DbCountryFake).Substring(2), DbSchemas.Test2.ToString()),
                    q => q.CountryId,
                    q => q.Code,
                    q => q.CreationDate,
                    q => q.LabelEN,
                    q => q.LabelFR,
                    q => q.LastModificationDate)
                .AddTuple(
                    "SelectCountry",
                    Field<DbCountryFake>(q => q.CountryId),
                    BdoDb.Field(nameof(DbCountryFake.Code), Table<DbCountryFake>()),
                    BdoDb.Field(nameof(DbCountryFake.CreationDate)),
                    BdoDb.Field(nameof(DbCountryFake.LabelEN)),
                    BdoDb.Field(nameof(DbCountryFake.LabelFR)),
                    BdoDb.Field(nameof(DbCountryFake.LastModificationDate)))
                .AddTuple(
                    "UpdateCountry",
                    BdoDb.Field(nameof(DbCountryFake.Code)),
                    BdoDb.Field(nameof(DbCountryFake.LabelEN)),
                    BdoDb.Field(nameof(DbCountryFake.LabelFR)),
                    BdoDb.Field(nameof(DbCountryFake.LastModificationDate)));

            // Country_Community

            AddTable("Country_Community", BdoDb.Table(nameof(DbCountry_CommunityFake).Substring(2), DbSchemas.Test2.ToString()))
                .AddTuple(
                    "SelectCountry_Community",
                    BdoDb.Field(nameof(DbCountry_CommunityFake.CountryId), Table("Country_Community")),
                    BdoDb.Field(nameof(DbCountry_CommunityFake.CommunityId), Table("Country_Community")))
                .AddTuple(
                    "UpdateCountry",
                    BdoDb.Field(nameof(DbCountry_CommunityFake.CountryId), Table("Country_Community")),
                    BdoDb.Field(nameof(DbCountry_CommunityFake.CommunityId), Table("Country_Community")));

            // CountryInformation

            AddTable<DbCountryInformationFake>(
                    BdoDb.Table(nameof(DbCountryInformationFake).Substring(2), DbSchemas.Test2.ToString()))
                .AddTuple(
                    "SelectCountryInformation",
                    BdoDb.Field(nameof(DbCountryInformationFake.CountryId)),
                    BdoDb.Field(nameof(DbCountryInformationFake.CreationDate)),
                    BdoDb.Field(nameof(DbCountryInformationFake.LastModificationDate)),
                    BdoDb.Field(nameof(DbCountryInformationFake.StartDate)))
                .AddTuple(
                    "UpdateCountryInformation",
                    BdoDb.Field(nameof(DbCountryInformationFake.LastModificationDate)),
                    BdoDb.Field(nameof(DbCountryInformationFake.StartDate)));

            // Relationships

            AddRelationship(
                    "Community_Country_Community",
                    Table<DbCommunityFake>(),
                    Table("Country_Community"),
                    (nameof(DbCommunityFake.CommunityId), nameof(DbCountry_CommunityFake.CommunityId)))
                .AddRelationship(
                    "Community_Country_Community",
                    Table<DbCountryFake>(),
                    Table("Country_Community"),
                    (nameof(DbCountryFake.CountryId), nameof(DbCountry_CommunityFake.CountryId)))
                .AddRelationship<DbCountryInformationFake, DbCountryFake>(
                    (q => q.CountryId, q => q.CountryId));
        }
    }
}

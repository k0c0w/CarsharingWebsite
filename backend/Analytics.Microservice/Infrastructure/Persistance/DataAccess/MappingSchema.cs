using Domain;
using LinqToDB.Mapping;
using Scheme = LinqToDB.Mapping.MappingSchema;

namespace Persistence.DataAccess;

public static class MappingSchema
{
    public readonly static Scheme TariffUsageScheme;

    static MappingSchema()
    {
        TariffUsageScheme = new Scheme(DatabaseConstants.DATABASE_NAME);

        var fluentBuilder = new FluentMappingBuilder(TariffUsageScheme);

        fluentBuilder.Entity<SubscriptionInfo>()
            .HasSchemaName(DatabaseConstants.DATABASE_NAME)
            .HasTableName(DatabaseConstants.TARIFFS_USAGE_TABLE_NAME)
            .Property(x => x.TariffName)
                .HasColumnName("tariff")
                .HasDbType("String")
                .IsNotNull()
            .Property(x => x.CarModelName)
                .HasColumnName("model")
                .HasDbType("String")
                .IsNotNull()
            .Property(x => x.CarLicensePlate)
                .HasColumnName("car_plate")
                .HasDbType("String")
                .IsNotNull()
            .Property(x => x.SubscriptionCreationDate)
                .HasColumnName("rent_creation_date")
                .HasDbType("Date")
                .IsNotNull()
            .Property(x => x.SubscriptionStartDate)
                .HasColumnName("rent_start_date")
                .HasDbType("Date")
                .IsNotNull();

        fluentBuilder.Build();
    }
}

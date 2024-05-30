using DB = Persistence.DataAccess.DatabaseConstants;


namespace Persistence.DataAccess;

public static class DatabaseScripts
{
    private const string TARIFFS_USAGE_TABLE_NAME = $"{DB.DATABASE_NAME}.{DB.TARIFFS_USAGE_TABLE_NAME}";

    public const string CREATE_TARIFFS_USAGE_SQL = $"CREATE TABLE IF NOT EXISTS {TARIFFS_USAGE_TABLE_NAME} (tariff String, model String, car_plate String, rent_creation_date Date, rent_start_date Date) ENGINE MergeTree PARTITION BY rent_creation_date ORDER BY rent_creation_date;";

    public const string CREATE_DATABASE_SQL = $"CREATE DATABASE IF NOT EXISTS {DB.DATABASE_NAME};";
}

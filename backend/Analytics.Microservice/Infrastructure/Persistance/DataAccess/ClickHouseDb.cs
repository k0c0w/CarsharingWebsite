using Domain;
using LinqToDB;
using LinqToDB.Data;

namespace Persistence.DataAccess;

public class ClickHouseDb : DataConnection
{
    public ClickHouseDb(DataOptions options) : base(options) { }

    public ITable<SubscriptionInfo> TariffsUsage => this.GetTable<SubscriptionInfo>();

    public ITable<TariffUsageSlice> TariffsUsageAggregation => this.GetTable<TariffUsageSlice>();
}

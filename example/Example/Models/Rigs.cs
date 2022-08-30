namespace Example.Models;

public partial class Rigs
{
    [JsonPropertyName("pagination")]
    public Pagination Pagination { get; set; }

    [JsonPropertyName("totalDevices")]
    public int TotalDevices { get; set; }

    [JsonPropertyName("totalRigs")]
    public int TotalRigs { get; set; }

    [JsonPropertyName("externalBalance")]
    public string ExternalBalance { get; set; }

    [JsonPropertyName("totalProfitabilityLocal")]
    public double TotalProfitabilityLocal { get; set; }

    [JsonPropertyName("nextPayoutTimestamp")]
    public DateTimeOffset NextPayoutTimestamp { get; set; }

    [JsonPropertyName("unpaidAmount")]
    public string UnpaidAmount { get; set; }

    [JsonPropertyName("totalProfitability")]
    public double TotalProfitability { get; set; }

    [JsonPropertyName("miningRigGroups")]
    public object[] MiningRigGroups { get; set; }

    [JsonPropertyName("miningRigs")]
    public MiningRig[] MiningRigs { get; set; }

    [JsonPropertyName("devicesStatuses")]
    public Statuses DevicesStatuses { get; set; }

    [JsonPropertyName("rigTypes")]
    public RigTypes RigTypes { get; set; }

    [JsonPropertyName("minerStatuses")]
    public Statuses MinerStatuses { get; set; }

    [JsonPropertyName("externalAddress")]
    public bool ExternalAddress { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("rigNhmVersions")]
    public object[] RigNhmVersions { get; set; }

    [JsonPropertyName("btcAddress")]
    public string BtcAddress { get; set; }
}

public partial class Statuses
{
    [JsonPropertyName("MINING")]
    public int Mining { get; set; }
}

public partial class MiningRig
{
    [JsonPropertyName("stats")]
    public Stat[] Stats { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("profitability")]
    public double Profitability { get; set; }

    [JsonPropertyName("unpaidAmount")]
    public string UnpaidAmount { get; set; }

    [JsonPropertyName("notifications")]
    public object[] Notifications { get; set; }

    [JsonPropertyName("rigId")]
    public string RigId { get; set; }

    [JsonPropertyName("localProfitability")]
    public double LocalProfitability { get; set; }

    [JsonPropertyName("minerStatus")]
    public string MinerStatus { get; set; }

    [JsonPropertyName("statusTime")]
    public long StatusTime { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public partial class Stat
{
    [JsonPropertyName("timeConnected")]
    public long TimeConnected { get; set; }

    [JsonPropertyName("speedRejectedR4NTime")]
    public double SpeedRejectedR4NTime { get; set; }

    [JsonPropertyName("xnsub")]
    public bool Xnsub { get; set; }

    [JsonPropertyName("algorithm")]
    public Algorithm Algorithm { get; set; }

    [JsonPropertyName("proxyId")]
    public long ProxyId { get; set; }

    [JsonPropertyName("speedRejectedTotal")]
    public double SpeedRejectedTotal { get; set; }

    [JsonPropertyName("speedRejectedR2Stale")]
    public double SpeedRejectedR2Stale { get; set; }

    [JsonPropertyName("speedAccepted")]
    public double SpeedAccepted { get; set; }

    [JsonPropertyName("profitability")]
    public double Profitability { get; set; }

    [JsonPropertyName("speedRejectedR1Target")]
    public double SpeedRejectedR1Target { get; set; }

    [JsonPropertyName("unpaidAmount")]
    public string UnpaidAmount { get; set; }

    [JsonPropertyName("difficulty")]
    public double Difficulty { get; set; }

    [JsonPropertyName("speedRejectedR5Other")]
    public double SpeedRejectedR5Other { get; set; }

    [JsonPropertyName("statsTime")]
    public long StatsTime { get; set; }

    [JsonPropertyName("speedRejectedR3Duplicate")]
    public double SpeedRejectedR3Duplicate { get; set; }

    [JsonPropertyName("market")]
    public string Market { get; set; }
}

public partial class Algorithm
{
    [JsonPropertyName("enumName")]
    public string EnumName { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public partial class Pagination
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("totalPageCount")]
    public int TotalPageCount { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }
}

public partial class RigTypes
{
    [JsonPropertyName("UNMANAGED")]
    public int Unmanaged { get; set; }
}
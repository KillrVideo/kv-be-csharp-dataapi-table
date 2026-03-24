using DataStax.AstraDB.DataApi.Tables;
using Cassandra;
using Newtonsoft.Json;

namespace kv_be_csharp_dataapi_table.Models;

public class VideoActivity
{
    [ColumnPrimaryKey(1)]
    [ColumnName("day")]
    [JsonProperty("day")]
    public DateOnly day  { get; set; } = DateOnly.Parse(DateTimeOffset.Now.Date.ToString("yyyy-MM-dd"));

    [ColumnPrimaryKey(2)]
    [ColumnName("watch_time")]
    [JsonProperty("watch_time")]
    public string watchTime { get; set; } = TimeUuid.NewId().ToString();

    [ColumnName("videoid")]
    public Guid videoid  { get; set; }

}
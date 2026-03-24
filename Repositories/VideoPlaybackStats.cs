using DataStax.AstraDB.DataApi.Tables;
using Newtonsoft.Json;

namespace kv_be_csharp_dataapi_table.Models;

public class VideoPlaybackStats
{
    [ColumnPrimaryKey(1)]
    [ColumnName("videoid")]
    [JsonProperty("video_id")]
    public Guid videoId { get; set; } = Guid.NewGuid();

    [ColumnName("complete_views")]
    [JsonProperty("complete_views")]
    public int completeViews { get; set; } = 0;

    [ColumnName("total_play_time")]
    [JsonProperty("total_play_time")]
    public int totalPlayTime { get; set; } = 0;
    
    [ColumnName("unique_viewers")]
    [JsonProperty("unique_viewers")]
    public int uniqueViewers { get; set; } = 0;

    [ColumnName("views")]
    [JsonProperty("views")]
    public int views { get; set; } = 0;
}
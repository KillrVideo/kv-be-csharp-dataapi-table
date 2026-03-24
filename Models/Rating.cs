using DataStax.AstraDB.DataApi.Tables;
using Newtonsoft.Json;

namespace kv_be_csharp_dataapi_table.Models;

// video_ratings_by_user
public class Rating
{
    [ColumnPrimaryKey(1)]
    [ColumnName("videoid")]
    [JsonProperty("video_id")]
    public Guid videoid { get; set; } = Guid.Empty;

    [ColumnPrimaryKey(2)]
    [ColumnName("userid")]
    public Guid userid { get; set; } = Guid.Empty;
    public int rating { get; set; } = 0;

    [ColumnName("rating_date")]
    [JsonProperty("rating_date")]
    public DateTimeOffset ratingDate { get; set; } = DateTimeOffset.Now;
}
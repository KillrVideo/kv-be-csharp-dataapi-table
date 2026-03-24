using DataStax.AstraDB.DataApi.Tables;
using Newtonsoft.Json;

namespace kv_be_csharp_dataapi_table.Models;

public class Video
{
    [ColumnPrimaryKey(1)]
    [ColumnName("videoid")]
    [JsonProperty("video_id")]
    public Guid videoId { get; set; } = Guid.NewGuid();

    [ColumnName("userid")]
    [JsonProperty("user_id")]
    public Guid userId { get; set; } = Guid.NewGuid();
    public string name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string location { get; set; } = string.Empty;

    [ColumnName("location_type")]
    [JsonProperty("location_type")]
    public int locationType { get; set; } = 0;
    
    [ColumnName("preview_image_location")]
    [JsonProperty("preview_image_location")]
    public string previewImageLocation { get; set; } = string.Empty;

    [ColumnName("content_features")]
    [JsonProperty("content_features")]
    public float[]? contentFeatures { get; set; }

    [ColumnName("added_date")]
    [JsonProperty("added_date")]
    public DateTime addedDate { get; set; } = DateTime.UtcNow;
    public HashSet<string> tags { get; set; } = new();
    public int views { get; set; } = 0;
    [ColumnName("youtube_id")]
    [JsonProperty("youtube_id")]
    public string youtubeId { get; set; } = string.Empty;
    [ColumnName("content_rating")]
    [JsonProperty("content_rating")]
    public string contentRating { get; set; } = string.Empty;
    public string category { get; set; } = string.Empty;
    public string language { get; set; } = string.Empty;
}
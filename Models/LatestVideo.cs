using DataStax.AstraDB.DataApi.Tables;

namespace kv_be_csharp_dataapi_table.Models;

public class LatestVideo
{
    [ColumnPrimaryKey(1)]
    [ColumnName("day")]
    public DateOnly day { get; set; } = DateOnly.Parse(DateTimeOffset.Now.Date.ToString("yyyy-MM-dd"));
    
    [ColumnPrimaryKey(2)]
    [ColumnName("added_date")]
    public DateTimeOffset addedDate { get; set; } = DateTimeOffset.Now;

    [ColumnPrimaryKey(3)]
    [ColumnName("videoid")]
    public Guid videoId { get; set; } = Guid.NewGuid();
    public string name { get; set; } = string.Empty;
    [ColumnName("preview_image_location")]
    public string previewImageLocation { get; set; } = string.Empty;
    public Guid userId { get; set; } = Guid.NewGuid();
    [ColumnName("content_rating")]
    public string contentRating { get; set; } = string.Empty;
    public string category { get; set; } = string.Empty;

    public static LatestVideo fromVideo(Video video)
    {
        LatestVideo response = new LatestVideo();
        response.videoId = video.videoId;
        response.name = video.name;
        response.previewImageLocation = video.previewImageLocation;
        response.addedDate = video.addedDate;
        response.userId = video.userId;
        response.contentRating = video.contentRating;
        response.category = video.category;

        return response;
    }
}
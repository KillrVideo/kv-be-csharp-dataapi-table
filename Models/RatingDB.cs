using DataStax.AstraDB.DataApi.Tables;

namespace kv_be_csharp_dataapi_table.Models;

// video_ratings table
public class RatingDB
{
    [ColumnPrimaryKey(1)]
    [ColumnName("videoid")]
    public Guid videoid { get; set; } = Guid.Empty;

    [ColumnName("rating_counter")]
    public int ratingCounter { get; set; } = 0;

    [ColumnName("rating_total")]
    public int ratingTotal { get; set; } = 0;

}
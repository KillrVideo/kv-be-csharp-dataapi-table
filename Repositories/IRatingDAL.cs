using kv_be_csharp_dataapi_table.Models;

namespace kv_be_csharp_dataapi_table.Repositories;

public interface IRatingDAL
{
    Task<RatingDB> SaveRating(RatingDB rating);
    Task<Rating> SaveRatingByUser(Rating rating);
    Task<Rating?> FindByVideoIdAndUserId(Guid videoid, Guid userid);
    Task<RatingDB?> FindByVideoId(Guid videoid);
    void UpdateRating(RatingDB rating);
    void UpdateRatingByUser(Rating rating);
}
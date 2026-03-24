using DataStax.AstraDB.DataApi.Core.Query;
using DataStax.AstraDB.DataApi.Core;
using kv_be_csharp_dataapi_table.Models;

namespace kv_be_csharp_dataapi_table.Repositories;

public class RatingDAL : IRatingDAL
{
    private readonly Database _database;

    public RatingDAL(ICassandraConnection cassandraConnection)
    {
        _database = cassandraConnection.GetDatabase();
    }

    public async Task<RatingDB?> FindByVideoId(Guid videoid)
    {
        var table = _database.GetTable<RatingDB>("video_ratings_no_counters");

        var filterBuilder = Builders<RatingDB>.Filter;
        var filter = filterBuilder.Eq("videoid", videoid);

        var ratings = await table.FindOneAsync(filter);

        return ratings;
    }

    public async Task<Rating?> FindByVideoIdAndUserId(Guid videoid, Guid userid)
    {
        var table = _database.GetTable<Rating>("video_ratings_by_user");

        var filterBuilder = Builders<Rating>.Filter;
        var filter = filterBuilder.And(
            filterBuilder.Eq("videoid", videoid),
            filterBuilder.Eq("userid", userid)
        );
        var rating = await table.FindOneAsync(filter);

        return rating;
    }

    public async Task<RatingDB> SaveRating(RatingDB rating)
    {
        var table = _database.GetTable<RatingDB>("video_ratings_no_counters");

        await table.InsertOneAsync(rating);

        return rating;
    }

    public async Task<Rating> SaveRatingByUser(Rating rating)
    {
        var table = _database.GetTable<Rating>("video_ratings_by_user");

        await table.InsertOneAsync(rating);

        return rating;
    }

    public async void UpdateRating(RatingDB rating)
    {
        var table = _database.GetTable<RatingDB>("video_ratings_no_counters");

        var filter = Builders<RatingDB>.Filter.Eq(r => r.videoid, rating.videoid);

        var update = Builders<RatingDB>.Update
            .Set(r => r.ratingCounter, rating.ratingCounter)
            .Set(r => r.ratingTotal, rating.ratingTotal);

        await table.UpdateOneAsync(filter,update);
    }

    public async void UpdateRatingByUser(Rating rating)
    {
        var table = _database.GetTable<Rating>("video_ratings_by_user");

        var filter = Builders<Rating>.Filter.CompositeKey(
            new PrimaryKeyFilter<Rating, Guid>(r => r.videoid, rating.videoid),
            new PrimaryKeyFilter<Rating, Guid>(r => r.userid, rating.userid)
        );

        var update = Builders<Rating>.Update
            .Set(r => r.rating, rating.rating)
            .Set(r => r.ratingDate, rating.ratingDate);

        await table.UpdateOneAsync(filter,update);
    }
}
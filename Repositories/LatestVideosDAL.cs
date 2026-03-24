using System.Threading.Tasks;
using DataStax.AstraDB.DataApi.Core;
using kv_be_csharp_dataapi_table.Models;

namespace kv_be_csharp_dataapi_table.Repositories;

public class LatestVideosDAL : ILatestVideosDAL
{
    private readonly Database _database;

    public LatestVideosDAL(ICassandraConnection cassandraConnection)
    {
        _database = cassandraConnection.GetDatabase();
    }

    public async Task<LatestVideo> SaveLatestVideo(LatestVideo video)
    {
        var table = _database.GetTable<LatestVideo>("latest_videos");
        await table.InsertOneAsync(video);

        return video;
    }

    public async Task<IEnumerable<LatestVideo>> GetLatestVideosToday(DateOnly day, int limit)
    {
        var table = _database.GetTable<LatestVideo>("latest_videos");

        var filterBuilder = Builders<LatestVideo>.Filter;
        var filter = filterBuilder.Eq("day", day);

        var latestVideosData = table.Find(filter).Limit(limit);

        return latestVideosData;
    }

    public async Task<IEnumerable<LatestVideo>> GetLatestVideos(int limit)
    {
        var table = _database.GetTable<LatestVideo>("latest_videos");

        var latestVideosData = table.Find().Limit(limit);

        return latestVideosData;
    }
}
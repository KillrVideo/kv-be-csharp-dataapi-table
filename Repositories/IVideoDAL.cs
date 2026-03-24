using kv_be_csharp_dataapi_table.Models;

namespace kv_be_csharp_dataapi_table.Repositories;
public interface IVideoDAL
{
    Task<Video?> GetVideoByVideoId(Guid videoId);

    Task<VideoPlaybackStats?> GetVideoPlaybackStatsByVideoId(Guid videoId);

    Task<IEnumerable<VideoActivity>> GetVideoActivity(DateOnly day);

    Video SaveVideo(Video video);

    Task UpdateVideo(Video video);

    Task UpdateVideoView(Video video);

    Task UpdateVideoPlaybackStats(VideoPlaybackStats video);

    Task InsertVideoActivity(VideoActivity video);

    Task<IEnumerable<Video>> GetByVector(float[]? vector, int limit);
}
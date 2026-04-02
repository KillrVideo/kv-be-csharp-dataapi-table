using kv_be_csharp_dataapi_table.Models;

namespace kv_be_csharp_dataapi_table.Repositories;

public interface ICommentDAL
{
    Task<Comment> SaveComment(Comment comment);
    Task<Comment> UpdateComment(Comment comment);
    Task<UserComment> SaveUserComment(UserComment comment);
    Task<UserComment> UpdateUserComment(UserComment comment);
    Task<Comment?> GetCommentById(Cassandra.TimeUuid commentid);
    Task<IEnumerable<Comment?>> GetCommentsByVideoId(Guid videoId, int limit);
    Task<IEnumerable<UserComment?>> GetCommentsByUserId(Guid userId);
    Task DeleteComment(Guid videoid, Cassandra.TimeUuid commentid);
    Task DeleteUserComment(Guid userid, Cassandra.TimeUuid commentid);
}
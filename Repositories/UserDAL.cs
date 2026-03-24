using DataStax.AstraDB.DataApi.Core;
using kv_be_csharp_dataapi_table.Models;

namespace kv_be_csharp_dataapi_table.Repositories;

public class UserDAL : IUserDAL
{
    private readonly Database _database;

    public UserDAL(ICassandraConnection cassandraConnection)
    {
        _database = cassandraConnection.GetDatabase();
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        var table = _database.GetTable<User>("users");

        var filterBuilder = Builders<User>.Filter;
        var filter = filterBuilder.Eq("email", email);

        var user = await table.FindOneAsync(filter);

        if (user is null)
        {
            return false;
        }
        return true;
    }

    public async Task<User?> FindByEmail(string email)
    {
        var table = _database.GetTable<User>("users");

        var filterBuilder = Builders<User>.Filter;
        var filter = filterBuilder.Eq("email", email);

        var user = await table.FindOneAsync(filter);

        return user;
    }

    public async Task<User?> FindByUserId(Guid userid)
    {
        var table = _database.GetTable<User>("users");

        var filterBuilder = Builders<User>.Filter;
        var filter = filterBuilder.Eq("userid", userid);

        var user = await table.FindOneAsync(filter);

        return user;
    }

    public User SaveUser(User user)
    {
        var table = _database.GetTable<User>("users");

        table.InsertOneAsync(user);

        return user;
    }

    public void UpdateUser(User user)
    {
        var table = _database.GetTable<User>("users");

        var filter = Builders<User>.Filter.Eq("userid", user.userid);

        var update = Builders<User>.Update
            .Set(u => u.accountStatus, user.accountStatus)
            .Set(u => u.createdDate, user.createdDate)
            .Set(u => u.email, user.email)
            .Set(u => u.firstname, user.firstname)
            .Set(u => u.lastname, user.lastname)
            .Set(u => u.lastLoginDate, user.lastLoginDate);

        table.UpdateOneAsync(filter, update);
    }
}
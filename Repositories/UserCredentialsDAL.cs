using DataStax.AstraDB.DataApi.Core;
using kv_be_csharp_dataapi_table.Models;

namespace kv_be_csharp_dataapi_table.Repositories;

public class UserCredentialsDAL : IUserCredentialsDAL
{
    private readonly Database _database;

    public UserCredentialsDAL(ICassandraConnection cassandraConnection)
    {
        _database = cassandraConnection.GetDatabase();
    }
    
    public UserCredentials? FindByEmail(string email)
    {
        var table = _database.GetTable<UserCredentials>("user_credentials");

        var filterBuilder = Builders<UserCredentials>.Filter;
        var filter = filterBuilder.Eq("email", email);

        UserCredentials userCreds = table.FindOne(filter);
        return userCreds;
    }

    public UserCredentials SaveUserCreds(UserCredentials user)
    {
        var table = _database.GetTable<UserCredentials>("user_credentials");

        table.InsertOne(user);

        return user;
    }

    public void UpdateUserCreds(UserCredentials user)
    {
        var table = _database.GetTable<UserCredentials>("user_credentials");

        var filter = Builders<UserCredentials>.Filter.Eq("email", user.email);

        var update = Builders<UserCredentials>.Update
            .Set(u => u.accountLocked, user.accountLocked)
            .Set(u => u.password, user.password);

        table.UpdateOne(filter,update);
    }
}
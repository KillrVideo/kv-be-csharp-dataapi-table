using DataStax.AstraDB.DataApi.Tables;

namespace kv_be_csharp_dataapi_table.Models;

public class User
{
    [ColumnPrimaryKey(1)]
    [ColumnName("userid")]
    public Guid userid { get; set; } = Guid.NewGuid();

    [ColumnName("created_date")]
    public DateTimeOffset createdDate { get; set; } = DateTimeOffset.Now;
    public string email { get; set; } = string.Empty;
    public string firstname { get; set; } = string.Empty;
    public string lastname { get; set; } = string.Empty;

    [ColumnName("account_status")]
    public string accountStatus { get; set; } = string.Empty;

    [ColumnName("last_login_date")]
    public DateTimeOffset lastLoginDate { get; set; } = DateTimeOffset.Now;

    public static User fromUserRegistrationRequest(UserRegistrationRequest req)
    {
        User user = new User();

        user.email = req.email;
        user.firstname = req.firstname;
        user.lastname = req.lastname;

        return user;
    }
}
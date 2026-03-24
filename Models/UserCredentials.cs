using System.ComponentModel.DataAnnotations;
using DataStax.AstraDB.DataApi.Tables;

namespace kv_be_csharp_dataapi_table.Models;

public class UserCredentials
{
    [ColumnPrimaryKey(1)]
    [ColumnName("email")]
    public string email { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public Guid userid { get; set; } = Guid.Empty;
    [ColumnName("account_locked")]
    public bool accountLocked { get; set; } = false;
}
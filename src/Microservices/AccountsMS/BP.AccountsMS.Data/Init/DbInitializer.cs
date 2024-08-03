using Dapper;
using Microsoft.Data.SqlClient;

namespace BP.AccountsMS.Data.Init
{
    public static class DbInitializer
    {
        public static async Task InitializaDatabaseAsync(string connectionString, string dbName)
        {
            using var connection = new SqlConnection(connectionString);
            var databases = await connection.QueryAsync(
                "SELECT * FROM sys.databases WHERE name = @Name",
                param: new { Name = dbName });

            if (!databases.Any())
            {
                await connection.ExecuteAsync($"CREATE DATABASE {dbName}");
            }
        }
    }
}

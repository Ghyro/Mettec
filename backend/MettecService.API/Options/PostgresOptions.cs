namespace MettecService.API.Options;

public class PostgresOptions
{
    public string Host { get; set; }
    public int Port { get; set; } 
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string GetConnectionString()
    {
        var builder = new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = Host,
            Port = Port,
            Database = Database,
            Username = Username,
            Password = Password
        };
        return builder.ConnectionString;
    }
}
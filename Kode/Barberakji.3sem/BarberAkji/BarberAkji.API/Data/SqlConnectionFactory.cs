using System.Data;
using Microsoft.Data.SqlClient;



public class SqlConnectionFactory // Simpel factory til at oprette SQL-forbindelser baseret på connection string i config
{
    private readonly IConfiguration _config;
    public SqlConnectionFactory(IConfiguration config) // Dependency injection af config for at hente connection string
    {
        _config = config;
    }

    public IDbConnection CreateConnection() // Returnerer en ny SqlConnection klar til brug
    {
        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    }
}

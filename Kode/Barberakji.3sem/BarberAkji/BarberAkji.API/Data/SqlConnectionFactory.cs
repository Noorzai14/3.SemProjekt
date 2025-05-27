using System.Data;
using System.Data.SqlClient;


public class SqlConnectionFactory
{
    private readonly IConfiguration _config;
    public SqlConnectionFactory(IConfiguration config)
    {
        _config = config;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    }
}

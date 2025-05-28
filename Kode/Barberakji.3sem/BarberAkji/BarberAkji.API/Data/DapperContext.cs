using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BarberAkji.API.Data
{

    
    public class DapperContext // DapperContext bruges til at oprette SQL-forbindelser til databasen.
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

 
        
        public IDbConnection CreateConnection() // Returnerer en åben SQL-forbindelse klar til brug i repositories.
        {
            return new SqlConnection(_connectionString);
        }
    }
}

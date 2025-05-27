using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BarberAkji.API.Data
{

    /// DapperContext bruges til at oprette SQL-forbindelser til databasen.
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

 
        /// Returnerer en åben SQL-forbindelse klar til brug i repositories.
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

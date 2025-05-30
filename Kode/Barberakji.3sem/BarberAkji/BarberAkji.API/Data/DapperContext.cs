﻿using System.Data;
using Microsoft.Data.SqlClient; 
using Microsoft.Extensions.Configuration;

namespace BarberAkji.API.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found!");

        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}

using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ProyectoArqSoft.Data
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection")
                ?? throw new InvalidOperationException("Connection string 'PostgresConnection' not found.");
        }

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
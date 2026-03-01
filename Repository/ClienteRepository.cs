using Npgsql;
using ProyectoArqSoft.Data;
using ProyectoArqSoft.Modelos;

namespace ProyectoArqSoft.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ClienteRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateClienteAsync(Cliente cliente)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                INSERT INTO cliente (tipo_cliente, nombre, ci, edad, sexo, telefono)
                VALUES (@tipo_cliente, @nombre, @ci, @edad, @sexo, @telefono)
                RETURNING id_cliente;";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@tipo_cliente", cliente.tipo_cliente);
            cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
            cmd.Parameters.AddWithValue("@ci", cliente.ci);
            cmd.Parameters.AddWithValue("@edad", (object?)cliente.edad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sexo", (object?)cliente.sexo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@telefono", (object?)cliente.telefono ?? DBNull.Value);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            var clientes = new List<Cliente>();

            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT id_cliente, tipo_cliente, nombre, ci, edad, sexo, telefono
                FROM cliente
                ORDER BY id_cliente;";

            using var cmd = new NpgsqlCommand(query, connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                clientes.Add(new Cliente
                {
                    id_cliente = reader.GetInt32(0),
                    tipo_cliente = reader.GetString(1),
                    nombre = reader.GetString(2),
                    ci = reader.GetString(3),
                    edad = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                    sexo = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    telefono = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                });
            }

            return clientes;
        }

        public async Task<List<Cliente>> SearchClientesAsync(string texto)
        {
            var clientes = new List<Cliente>();

            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT id_cliente, tipo_cliente, nombre, ci, edad, sexo, telefono
                FROM cliente
                WHERE nombre ILIKE @texto
                   OR ci ILIKE @texto
                   OR telefono ILIKE @texto
                ORDER BY id_cliente;";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@texto", $"%{texto}%");

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                clientes.Add(new Cliente
                {
                    id_cliente = reader.GetInt32(0),
                    tipo_cliente = reader.GetString(1),
                    nombre = reader.GetString(2),
                    ci = reader.GetString(3),
                    edad = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                    sexo = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    telefono = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                });
            }

            return clientes;
        }
    }
}
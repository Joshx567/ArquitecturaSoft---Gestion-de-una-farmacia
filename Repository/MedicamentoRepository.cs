using Npgsql;
using ProyectoArqSoft.Data;
using ProyectoArqSoft.Modelos;
using System.Data;

namespace ProyectoArqSoft.Repository
{
    public class MedicamentoRepository : IMedicamentoRepository
    {
        private readonly DbConnectionFactory _db;

        public MedicamentoRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<int> CrearAsync(Medicamento medicamento)
        {
            const string sql = @"
                INSERT INTO medicamento (nombre, presentacion, clasificacion, concentracion, stock_minimo)
                VALUES (@nombre, @presentacion, @clasificacion, @concentracion, @stock_minimo)
                RETURNING id_medicamento;";

            // CORRECCIÓN: Convertir a NpgsqlConnection
            using var conn = (NpgsqlConnection)_db.CreateConnection();
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nombre", medicamento.nombre);
            cmd.Parameters.AddWithValue("@presentacion", medicamento.presentacion);
            cmd.Parameters.AddWithValue("@clasificacion", medicamento.clasificacion);
            cmd.Parameters.AddWithValue("@concentracion", medicamento.concentracion);
            cmd.Parameters.AddWithValue("@stock_minimo", medicamento.stock_minimo);

            var newId = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(newId);
        }

        public async Task<List<Medicamento>> ListarAsync()
        {
            var lista = new List<Medicamento>();

            const string sql = @"
                SELECT id_medicamento, nombre, presentacion, clasificacion, concentracion, stock_minimo
                FROM medicamento
                ORDER BY id_medicamento DESC;";

            // CORRECCIÓN: Convertir a NpgsqlConnection y agregar concentracion al SELECT
            using var conn = (NpgsqlConnection)_db.CreateConnection();
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Medicamento
                {
                    id_medicamento = reader.GetInt32(0),
                    nombre = reader.GetString(1),
                    presentacion = reader.GetString(2),
                    clasificacion = reader.GetString(3),
                    concentracion = reader.GetString(4),  // CORRECCIÓN: Agregar concentracion
                    stock_minimo = reader.GetInt32(5)
                });
            }

            return lista;
        }
    }
}
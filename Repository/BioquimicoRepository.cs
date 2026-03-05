using Npgsql;
using ProyectoArqSoft.Data;
using ProyectoArqSoft.Modelos;

namespace ProyectoArqSoft.Repository
{
    public class BioquimicoRepository : IBioquimicoRepository
    {
        private readonly DbConnectionFactory _db;

        public BioquimicoRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<bool> ExisteCiAsync(string ci)
        {
            const string sql = @"SELECT EXISTS(SELECT 1 FROM bioquimico WHERE ci = @ci);";

            await using var conn = _db.CreateConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ci", ci);

            var result = await cmd.ExecuteScalarAsync();
            return result is bool b && b;
        }

        public async Task<int> CrearAsync(Bioquimico b)
        {
            const string sql = @"
                INSERT INTO bioquimico (nombres, apellidos, ci, telefono, activo)
                VALUES (@nombres, @apellidos, @ci, @telefono, @activo)
                RETURNING id_bioquimico;";

            await using var conn = _db.CreateConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nombres", b.nombres);
            cmd.Parameters.AddWithValue("@apellidos", b.apellidos);
            cmd.Parameters.AddWithValue("@ci", b.ci);
            cmd.Parameters.AddWithValue("@telefono", b.telefono);
            cmd.Parameters.AddWithValue("@activo", b.activo);

            var newId = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(newId);
        }

        public async Task<List<Bioquimico>> ListarAsync()
        {
            var lista = new List<Bioquimico>();

            const string sql = @"
                SELECT id_bioquimico, nombres, apellidos, ci, telefono, activo
                FROM bioquimico
                ORDER BY apellidos ASC, nombres ASC;";

            await using var conn = _db.CreateConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Bioquimico
                {
                    id_bioquimico = reader.GetInt32(0),
                    nombres = reader.GetString(1),
                    apellidos = reader.GetString(2),
                    ci = reader.GetString(3),
                    telefono = reader.GetString(4),
                    activo = reader.GetBoolean(5)
                });
            }

            return lista;
        }
    }
}
using System.Data.Common;
using Npgsql;
using ProyectoArqSoft.Data;
using ProyectoArqSoft.Modelos;

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

            await using var conn = _db.CreateConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nombre", medicamento.nombre);
            cmd.Parameters.AddWithValue("@presentacion", medicamento.presentacion);
            cmd.Parameters.AddWithValue("@clasificacion", medicamento.clasificacion);
            cmd.Parameters.AddWithValue("@concentracion",medicamento.concentracion); 
            cmd.Parameters.AddWithValue("@stock_minimo", medicamento.stock_minimo);

            var newId = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(newId); 
        }
    }
}

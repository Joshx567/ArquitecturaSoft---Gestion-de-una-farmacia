using Dapper;
using ProyectoArqSoft.Data;
using ProyectoArqSoft.Model.Bioquimico;

namespace ProyectoArqSoft.Service
{
    public class BioquimicoService : IBioquimicoService
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public BioquimicoService(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        // --- LÓGICA DE BÚSQUEDA (Ya la tenías, está perfecta) ---
        public async Task<IEnumerable<Bioquimico>> BuscarAsync(string filtro)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            
            if (string.IsNullOrWhiteSpace(filtro))
            {
                string sqlAll = @"SELECT * FROM ""bioquimico"" ORDER BY nombres ASC";
                return await connection.QueryAsync<Bioquimico>(sqlAll);
            }

            string sqlFilter = @"SELECT * FROM ""bioquimico"" 
                                 WHERE ""nombres"" ILIKE @f 
                                 OR ""apellidos"" ILIKE @f 
                                 OR ""ci"" ILIKE @f 
                                 ORDER BY nombres ASC";
            
            return await connection.QueryAsync<Bioquimico>(sqlFilter, new { f = $"%{filtro}%" });
        }

        
        public async Task<bool> EliminarAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            
            
            string sql = @"DELETE FROM ""bioquimico"" WHERE ""id_bioquimico"" = @id";
            
            
            int filasAfectadas = await connection.ExecuteAsync(sql, new { id });
            
            
            return filasAfectadas > 0;
        }
    }
}
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

    }
}
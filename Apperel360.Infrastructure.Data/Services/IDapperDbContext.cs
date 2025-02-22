using Apperel360.Domain.DBConNameRepositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apperel360.Infrastructure.Data.Services
{
    public interface IDapperDbContext
    {
        IDbConnection GetDbConnection(DatabaseConnectionName connectionName);
        T Get<T>(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure, int commandTimeout = 0);
        List<T> GetAll<T>(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure, int commandTimeout = 0);

        T ExecuteGet<T>(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure);
        List<T> ExecuteGetAll<T>(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure);
        int Execute(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure);

    }
}

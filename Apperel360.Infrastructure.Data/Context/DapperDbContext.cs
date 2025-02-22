using Apperel360.Domain.DBConNameRepositories;
using Apperel360.Infrastructure.Data.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Apperel360.Infrastructure.Data.Context
{
    public class DapperDbContext : IDapperDbContext
    {
        private readonly IDictionary<DatabaseConnectionName, string> _connetionDict;
        public DapperDbContext(IDictionary<DatabaseConnectionName, string> connetionDict)
        {
            _connetionDict = connetionDict;
        }    
        public int Execute(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection con = GetDbConnection(connectionName))
            {
                return con.Execute(sp, dynamicParameters, commandType: commandType);
            }
        }

        public T ExecuteGet<T>(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection con = GetDbConnection(connectionName))
            {
                return con.Query<T>(sp, dynamicParameters, commandType: commandType).FirstOrDefault();
            }
        }

        public List<T> ExecuteGetAll<T>(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection con = GetDbConnection(connectionName))
            {
                return con.Query<T>(sp, dynamicParameters, commandType: commandType).ToList();
            }
        }

        public T Get<T>(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure, int commandTimeout = 0)
        {
            using (IDbConnection con = GetDbConnection(connectionName))
            {
                return con.Query<T>(sp, dynamicParameters, commandType: commandType, commandTimeout: commandTimeout).FirstOrDefault();
            }
        }

        public List<T> GetAll<T>(string sp, DynamicParameters dynamicParameters, DatabaseConnectionName connectionName = DatabaseConnectionName.Apperel360App, CommandType commandType = CommandType.StoredProcedure, int commandTimeout = 0)
        {
            using (IDbConnection con = GetDbConnection(connectionName))
            {
                return con.Query<T>(sp,dynamicParameters,commandType:commandType).ToList();
            }
        }

        public IDbConnection GetDbConnection(DatabaseConnectionName connectionName)
        {
            string connetionString = string.Empty;
            if (_connetionDict.TryGetValue(connectionName, out connetionString))
            {
                return new SqlConnection(connetionString);
            }
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace webAPI_testTask
{
    public class DataBaseConnection
    {
        private DataBaseSettings _dbSettings;
        public DataBaseConnection(IOptions<DataBaseSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }
        public IDbConnection CreateConnection()
        {
            var connectionString = $"Host={_dbSettings.Server};" +
                $"Port={_dbSettings.Port};" +
                $"Database={_dbSettings.Database};" +
                $"Username={_dbSettings.Username};" +
                $"Password={_dbSettings.Password};";
            return new NpgsqlConnection(connectionString);
        }
    }
}

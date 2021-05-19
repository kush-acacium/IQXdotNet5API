using System.Data.Odbc;

using Microsoft.Extensions.Configuration;

namespace IQXdotNet5API.Db
{
    public class IQXDbClient : IIQXDbClient
    {
        private readonly string _connectionString;

        public IQXDbClient(IConfiguration config)
        {
            _connectionString = string.Format(config["IqxDb:ConnectionString"]);
        }

        public OdbcConnection GetConnection()
        {
            return new(_connectionString);
        }
    }
}

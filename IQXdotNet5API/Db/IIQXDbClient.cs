using System.Data.Odbc;

namespace IQXdotNet5API.Db
{
    public interface IIQXDbClient
    {
        public OdbcConnection GetConnection();
    }
}

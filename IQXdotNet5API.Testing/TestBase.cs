using IQXdotNet5API.Db;

using Microsoft.Extensions.Configuration;

using NUnit.Framework;

namespace IQXdotNet5API.Testing
{
    public abstract class TestBase
    {
        protected IIQXDbClient Db;

        [SetUp]
        public void SetUpIqxDb()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();
            Db = new IQXDbClient(config);
        }
    }
}
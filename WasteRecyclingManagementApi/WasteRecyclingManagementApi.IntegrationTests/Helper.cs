using Microsoft.Extensions.Configuration;

namespace WasteRecyclingManagementApi.Tests
{
    public class Helper
    {
        private static Helper _instance;
        public static Helper Instance { get { return _instance ??= new Helper(); } }
        public IConfigurationRoot Configuration { get; set; }

        public Helper()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public string GetConnectionString()
        {
            return Configuration.GetConnectionString("dockerDatabase");
        }
    }
}

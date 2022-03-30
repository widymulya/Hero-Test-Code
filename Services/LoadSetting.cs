using Microsoft.Extensions.Configuration;

namespace Hero_Code_Test.Services
{

    public class LoadSetting
    {
        public static IConfigurationRoot LoadSettings()
        {
            try
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

                string baseUrl = config.GetSection("BaseUrl").Value;
                string apiKey = config.GetSection("apiKey").Value;
                

                if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(apiKey))
                {
                    return null;
                }



                return config;
            }
            catch (System.IO.FileNotFoundException)
            {
                return null;
            }
        }
    }
}
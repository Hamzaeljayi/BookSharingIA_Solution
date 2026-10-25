using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public static class DataSettings
    {
        public static string GetConnectionString(string name)
        {
            string json = File.ReadAllText("jsconfig1.json");
            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;

            if (root.TryGetProperty("ConnectionStrings", out JsonElement connectionStrings))
            {
                if (connectionStrings.TryGetProperty(name, out JsonElement connection))
                {
                    string? value = connection.GetString();
                    if (value is null)
                        throw new InvalidOperationException($"La valeur de la chaîne de connexion '{name}' est null dans jsconfig1.json.");
                    return value;
                }
                else
                {
                    throw new KeyNotFoundException($"La clé '{name}' n'existe pas dans ConnectionStrings.");
                }
            }
            else
            {
                throw new Exception("La section 'ConnectionStrings' est manquante dans jsconfig1.json.");
            }
        }

        public static string GetAdminPassword()
        {
            var config = LoadConfig();
            var password = config["AdminPassword"];
            if (password is null)
                throw new InvalidOperationException("La clé 'AdminPassword' est manquante dans jsconfig1.json.");
            return password;
        }

        private static IConfigurationRoot LoadConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("jsconfig1.json")  // 🔧 Correction ici
                .Build();
        }
    }
}
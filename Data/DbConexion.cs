using System.Data.SqlClient;

namespace ReportesMVC.Data
{
    public class DbConexion
    {
        private string connection = string.Empty;

        public DbConexion() 
        { 
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            connection = builder.GetSection("ConnectionStrings:ConnectionString").Value;

        }

        public string getCadenaSql()
        {
            return connection;
        }

    }
    
}

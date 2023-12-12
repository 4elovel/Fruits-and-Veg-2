using System.Data.Common;

namespace Fruits_and_Veg_2
{
    internal class DbConnectionHolder
    {
        public DbConnection connection { get; set; }
        public DbConnectionHolder(string providerName, string connectionString, DbProviderFactory dbProvider)
        {
            CreateDbConnection(providerName, connectionString, dbProvider);
        }
        protected void CreateDbConnection(string providerName, string connectionString, DbProviderFactory dbProvider)
        {
            DbProviderFactories.RegisterFactory(providerName, dbProvider);

            DbConnection? connection = null;


            if (connectionString != null)
            {
                try
                {

                    DbProviderFactory factory =
                        DbProviderFactories.GetFactory(providerName);

                    connection = factory.CreateConnection();
                    connection!.ConnectionString = connectionString;
                }
                catch (Exception ex)
                {
                    // Set the connection to null if it was created.
                    if (connection != null)
                    {
                        connection = null;
                    }
                    Console.WriteLine(ex.Message);
                }
            }
            // Return the connection.
            this.connection = connection!;
        }
    }
}

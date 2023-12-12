using Fruits_and_Veg_2;
using System.Configuration;
using System.Data.Common;
using System.Text;

internal class Program
{
    private static string SQLiteConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
    private static string MySqlConnectionString = ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString;
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        string queryString = "select * from FruitsAndVegetables";
        DbConnectionHolder SQLiteConnectionHolder = new DbConnectionHolder("System.Data.SQLite", SQLiteConnectionString, System.Data.SQLite.SQLiteFactory.Instance);
        DbConnectionHolder MySqlConnectionHolder = new DbConnectionHolder("MySql.Data.MySqlClient", MySqlConnectionString, System.Data.SqlClient.SqlClientFactory.Instance);

        SQLiteConnectionHolder.connection.Open();
        DbCommand command = SQLiteConnectionHolder.connection.CreateCommand();
        command.CommandText = queryString;

        DbDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine("{0}. {1}", reader[0], reader[1]);
        }
        SQLiteConnectionHolder.connection.Close();
    }


    private static async void CreateTableAsync(DbConnection connection)
    {
        string sql = "create table if not exists FruitsAndVegetables (name TEXT,type TEXT,color TEXT,calory INTEGER)";
        var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync();
    }

    private static async void CreateOBJAsync(DbConnection connection, string name, string type, string color, string calory)
    {
        await connection.OpenAsync();
        var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = "INSERT INTO FruitsAndVegetables (name, type, color, calory) VALUES ($name, $type, $color, $calory)";

        insertCommand.AddParameterWithValue("$name", name);
        insertCommand.AddParameterWithValue("$type", type);
        insertCommand.AddParameterWithValue("$color", color);
        insertCommand.AddParameterWithValue("$calory", calory);

        insertCommand.ExecuteNonQuery();
        await connection.CloseAsync();
    }
    private static async void SelAllAsync(DbConnection connection)
    {
        await connection.OpenAsync();
        var Command = connection.CreateCommand();
        Command.CommandText = "select * from FruitsAndVegetables";
        await Command.ExecuteNonQueryAsync();
        using var reader = await Command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Console.WriteLine($"{reader["name"]} {reader["type"]} {reader["color"]} {reader["calory"]}");
        }
        await connection.CloseAsync();
    }

    private static async void SelNamesAsync(DbConnection connection)
    {
        await connection.OpenAsync();

        var Command = connection.CreateCommand();
        Command.CommandText = "select * from FruitsAndVegetables";
        await Command.ExecuteNonQueryAsync();
        using var reader = await Command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Console.WriteLine($"{reader["name"]}");
        }
        await connection.CloseAsync();

    }
    private static async void SelColorsAsync(DbConnection connection)
    {
        await connection.OpenAsync();
        var Command = connection.CreateCommand();
        Command.CommandText = "select * from FruitsAndVegetables";
        await connection.CloseAsync();
        using var reader = await Command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Console.WriteLine($"{reader["color"]}");
        }
        await connection.CloseAsync();
    }//там далі копі паст попередньої дз з переробленням на async методи

}
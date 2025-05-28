using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.IO;

namespace RecipeBookApp
{
    public static class DatabaseHelper
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes.db");
        private static string connectionString = $"Data Source={dbPath}";

        public static void InitializeDatabase()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Recipes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Instructions TEXT NOT NULL
                );";
            command.ExecuteNonQuery();
        }

        public static void InsertRecipe(Recipe recipe)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Recipes (Name, Instructions)
                VALUES ($name, $instructions);";
            command.Parameters.AddWithValue("$name", recipe.Name);
            command.Parameters.AddWithValue("$instructions", recipe.Instructions);
            command.ExecuteNonQuery();
        }

        public static void DeleteRecipe(string name)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                DELETE FROM Recipes WHERE Name = $name;";
            command.Parameters.AddWithValue("$name", name);
            command.ExecuteNonQuery();
        }

        public static ObservableCollection<Recipe> LoadRecipes()
        {
            var recipes = new ObservableCollection<Recipe>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Name, Instructions FROM Recipes;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                recipes.Add(new Recipe
                {
                    Name = reader.GetString(0),
                    Instructions = reader.GetString(1)
                });
            }

            return recipes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

/* I'd like to thank my good friend chat gpt for helping me with this fucking shit */

namespace RecipeApp2025.Resources.Classes
{
    public class DatabaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseHelper()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "recipeapp.db");
            _database = new SQLiteAsyncConnection(dbPath);
            //_database.CreateTableAsync<Recipe>().Wait();
            _database.CreateTableAsync<User>().Wait();
        }


        /* This method allows us to insert or update an object in the database I guess */
        public Task<int> SaveObjectAsync<T>(T obj) where T : new()
        {
            return _database.InsertOrReplaceAsync(obj);
        }

        /* This method will allow us to retrieve all the objects we need */
        public Task<List<Recipe>> GetObjectsAsync()
        {
            return _database.Table<Recipe>().ToListAsync();
        }

        /* allows us to delete a object or some shit */
        public Task<int> DeleteObjectAsync(Recipe obj)
        {
            return _database.DeleteAsync(obj);
        }

        /* Clear all data */
        public Task<int> ClearDatabaseAsync()
        {
            return _database.DeleteAllAsync<Recipe>();
        }
        public Task<User> GetUserAsync(string username)
        {
            return _database.Table<User>().FirstOrDefaultAsync(u => u.Username == username);
        }

    }
        
}

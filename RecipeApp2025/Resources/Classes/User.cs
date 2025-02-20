using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RecipeApp2025.Resources.Classes
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Recipe> UserRecipes { get; set; }
    }
}

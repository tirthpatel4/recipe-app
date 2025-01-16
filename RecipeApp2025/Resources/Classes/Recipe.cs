using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp2025.Resources.Classes
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Img_url { get; set; }
        public int Total_time { get; set; }
        public int Num_people_served { get; set; }
        public bool isSaved { get; set; }
        public Recipe(string name)
        {
            Name = name;
            Img_url = "https://img.spoonacular.com/recipes/716429-90x90.jpg";
            Num_people_served = 2;
            Total_time = 45;
            isSaved = false;
        }
    }
}

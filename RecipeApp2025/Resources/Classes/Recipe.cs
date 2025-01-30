using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace RecipeApp2025.Resources.Classes 
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement] //Auto-incrementing ID
        public int Id { get; set; }

        public string Name { get; set; }
        public string Large_img_url { get; set; }
        public string Img_url { get; set; }
        public int Total_time { get; set; }
        public int Cook_time { get; set; }
        public int Prep_time { get; set; }
        public int Num_people_served { get; set; }
        public bool isSaved { get; set; }


        /* No args constructor */
        public Recipe()
        {
            Name = "No Name";
            Img_url = "https://img.spoonacular.com/recipes/716429-90x90.jpg";
            Large_img_url = "https://img.spoonacular.com/recipes/1697885-556x370.jpg";
            Num_people_served = 2;
            Total_time = 45;
            Cook_time = 25;
            Prep_time = 20;
            isSaved = false;
        }
        public Recipe(string name)
        {
            Name = name;
            Img_url = "https://img.spoonacular.com/recipes/716429-90x90.jpg";
            Large_img_url = "https://img.spoonacular.com/recipes/1697885-556x370.jpg";
            Num_people_served = 2;
            Total_time = 45;
            Cook_time = 25;
            Prep_time = 20;
            isSaved = false;
        }
    }

    public class Ingredient
    {
        public string I_Name { get; set; }
        public int I_Count { get; set; }
        public int I_Amount { get; set; }
        public string I_Unit { get; set; }

        public Ingredient(string name, int count, int amount, string unit)
        {
            I_Name = name;
            I_Count = count;
            I_Amount = amount;
            I_Unit = unit;
        }
    }
}

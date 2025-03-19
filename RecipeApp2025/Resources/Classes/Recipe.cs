using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using RecipeApp2025.Resources.Classes;

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

        public List<String> Steps_List { get; set; } = new List<string>() { "step 1", "step2", "step3" };
        public List<Ingredient> Ingredients_List { get; set; }
        public bool IsIngredientListVisible { get; set; }
        public bool IsStepsListVisible { get; set; }

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
            Ingredients_List = new List<Ingredient>();
        }
        public Recipe(string name)
        {
            /*For testing. These fields will later be popluated with actual data*/
            Name = name;
            Img_url = "https://img.spoonacular.com/recipes/716429-90x90.jpg";
            /*ALL recipes have images in these two sizes. */
            Large_img_url = "https://img.spoonacular.com/recipes/1697885-556x370.jpg";
            Num_people_served = 2;
            Total_time = 45;
            Cook_time = 25;
            Prep_time = 20;
            isSaved = false;
            Steps_List = new List<String>();
            Ingredients_List = new List<Ingredient>();
            IsIngredientListVisible = true;



        }


    }

    public class Ingredient
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string Image { get; set; }
        public string Full { get; set; }

        public Ingredient(string n, double a, string u, string im, string full)
        {
            Name = n;
            Amount = a;
            Unit = u;
            Image = im;
            Full = full;
        }
    }

    public class Step
    {
        public string Text { get; set; }
        public List<String> Needed_Ingredients { get; set; }
        public Step(String text)
        {
            Text = text;
            Needed_Ingredients = new List<String>();
        }
}

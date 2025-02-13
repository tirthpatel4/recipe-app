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

        public List<String> steps { get; set; }
        public List<String> ingredients { get; set; }
        public bool IsIngredientListVisible { get; set; }
        public bool IsStepsListVisible { get; set; }


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
            steps = new List<String>();
            ingredients = new List<String>();
            IsIngredientListVisible = true;

            
            steps.Add("Remove any fat and gristle from chuck roast; cut into strips ½-inch thick by 2-inches long. Season with ½ teaspoon salt and ½ teaspoon pepper.");
            steps.Add("Melt butter in a large skillet over medium heat. Add beef and brown quickly.");
            steps.Add("Push beef to one side of the skillet. Add onions; cook and stir for 3 to 5 minutes, then push to the side with beef.");
            steps.Add("Stir flour into juices on the empty side of the pan. Pour in beef broth and bring to a boil, stirring constantly. Lower the heat and stir in mustard. Cover and simmer for 1 hour or until the beef is tender.");
            steps.Add("Five minutes before serving, stir in mushrooms, sour cream, and white wine. Cook until heated through; season with salt and pepper.");


            ingredients.Add("2 pounds beef chuck roast");
            ingredients.Add("½ teaspoon salt");
            ingredients.Add("½ teaspoon ground black pepper");
            ingredients.Add("4 ounces butter");
            ingredients.Add("4 green onions, sliced (white parts only)");
            ingredients.Add("4 tablespoons all-purpose flour");
            ingredients.Add("1 (10.5 ounce) can condensed beef broth");
            ingredients.Add("1 teaspoon prepared mustard");
            ingredients.Add("1 (6 ounce) can sliced mushrooms, drained");
            ingredients.Add("⅓ cup sour cream");
            ingredients.Add("⅓ cup white wine");
            ingredients.Add("salt and ground black pepper to taste");
           
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

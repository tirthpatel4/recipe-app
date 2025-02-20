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

<<<<<<< Updated upstream
<<<<<<< Updated upstream

        /* No args constructor */
        public Recipe()
        {
            Name = "No Name";
=======
=======
>>>>>>> Stashed changes
        public List<String> steps { get; set; }
        public List<String> ingredients { get; set; }
        public bool IsIngredientListVisible { get; set; }
        public bool IsStepsListVisible { get; set; }

        public Recipe(string name)
        {
            /*For testing. These fields will later be popluated with actual data*/
            Name = name;
>>>>>>> Stashed changes
            Img_url = "https://img.spoonacular.com/recipes/716429-90x90.jpg";
            /*ALL recipes have images in these two sizes. */
            Large_img_url = "https://img.spoonacular.com/recipes/1697885-556x370.jpg";
            Num_people_served = 2;
            Total_time = 45;
            Cook_time = 25;
            Prep_time = 20;
            isSaved = false;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            Ingredients_List = new List<Ingredient>();
        }

        public List<String> steps { get; set; } = new List<string>() {"step 1", "step2", "step3"};
        public List<Ingredient> Ingredients_List { get; set; }
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
            Ingredients_List = new List<Ingredient>();
            IsIngredientListVisible = true;
=======
=======
>>>>>>> Stashed changes
            steps = new List<String>();
            ingredients = new List<String>();
            IsIngredientListVisible = false;
            IsIngredientListVisible = false;
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

            
            steps.Add("Remove any fat and gristle from chuck roast; cut into strips ½-inch thick by 2-inches long. Season with ½ teaspoon salt and ½ teaspoon pepper.");
            steps.Add("Melt butter in a large skillet over medium heat. Add beef and brown quickly.");
            steps.Add("Push beef to one side of the skillet. Add onions; cook and stir for 3 to 5 minutes, then push to the side with beef.");
            steps.Add("Stir flour into juices on the empty side of the pan. Pour in beef broth and bring to a boil, stirring constantly. Lower the heat and stir in mustard. Cover and simmer for 1 hour or until the beef is tender.");
            steps.Add("Five minutes before serving, stir in mushrooms, sour cream, and white wine. Cook until heated through; season with salt and pepper.");


<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
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
}

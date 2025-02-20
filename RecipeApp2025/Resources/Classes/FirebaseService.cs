using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeApp2025.Resources.Classes;
using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;

public class FirebaseService
{
    private readonly FirebaseClient _firebaseClient;

    public FirebaseService()
    {
        _firebaseClient = new FirebaseClient("https://recipeapp2025-default-rtdb.firebaseio.com/");
    }

    public async Task AddRecipe(Recipe recipe)
    {
        await _firebaseClient.Child("recipes").PostAsync(recipe);
    }

    public async Task AddUser(User u)
    {
        u.Id = Guid.NewGuid().ToString();

        await _firebaseClient.Child("users").Child(u.Id).PutAsync(u);
    }
    public async Task AddSavedRecipe(Recipe r, string username)
    {
        var verify = await GetUser(username);
        if (verify != null)
        {
            await _firebaseClient.Child("users").Child(verify.Id.ToString()).Child("savedrecipes").PostAsync(r);
        }
        
    }

    public async Task AddUniqueRecipes(List<Recipe> recipes)
    {
        // Fetch existing recipes from Firebase
        var existingRecipes = (await _firebaseClient.Child("recipes")
            .OnceAsync<Recipe>())
            .Select(item => item.Object.Name)
            .ToList();

        // Filter out recipes that already exist
        var newRecipes = recipes.Where(r => !existingRecipes.Contains(r.Name)).ToList();

        // Add only the new recipes to Firebase
        foreach (var recipe in newRecipes)
        {
            await _firebaseClient.Child("recipes").PostAsync(recipe);
        }
    }
    public async Task<List<Recipe>> GetRecipes()
    {
        return (await _firebaseClient.Child("recipes")
            .OnceAsync<Recipe>())
            .Select(item => item.Object)
            .ToList();
    }
    public async Task<User> GetUser(string username)
    {
        var users = await _firebaseClient
        .Child("users")
        .OrderBy("Username")
        .EqualTo(username)
        .OnceAsync<User>();

        return users.FirstOrDefault()?.Object;
    }

    // Function to remove a recipe by passing a Recipe object
    public async Task RemoveRecipe(Recipe recipe, string username)
    {
        var verify = await GetUser(username);
        if (verify != null)
        {
            // Fetch all recipes from Firebase
            var recipes = await _firebaseClient.Child("users").Child(verify.Id.ToString()).Child("savedrecipes").OnceAsync<Recipe>();

            // Find the matching recipe based on the Name (or any other unique identifier)
            var recipeToRemove = recipes.FirstOrDefault(r => r.Object.Name == recipe.Name);

            if (recipeToRemove != null)
            {
                // Delete the found recipe from Firebase
                await _firebaseClient.Child("users").Child(verify.Id.ToString()).Child("savedrecipes").Child(recipeToRemove.Key).DeleteAsync();
            }
        }
    }
}
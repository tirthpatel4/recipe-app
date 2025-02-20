using System;
using Newtonsoft.Json.Linq;
using RecipeApp2025.Resources.Classes;
using System.Diagnostics;
using Syncfusion.Maui.Toolkit.PullToRefresh;

namespace RecipeApp2025.Services
{
	public class RecipeService
	{
		private const string ApiKey1 = "b58ecd4c96354452b244c80df3e5da60";
		private const string ApiKey = "60bd29f7cfc54795868a9a053cb447a3";

        private const string BaseUrl = "https://api.spoonacular.com/";
		private const string STANDARD_UNITS = "us";
		HttpClient client;

		
		public RecipeService()
		{
			client = new HttpClient();
		}

		public async Task<List<Recipe>> GetRecipesAsync()
		{
			Debug.WriteLine("hello");
			var url = $"{BaseUrl}recipes/complexSearch?apiKey={ApiKey}&addRecipeInformation=true&number=5";
			var response = await client.GetAsync(url);
            Debug.WriteLine($"getrecasync API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");

            if (response.IsSuccessStatusCode)
			{
                Debug.WriteLine("getrec success");

                var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content);
				var recipes = json["results"]?.Select(r => new Recipe
				{
					Id = (int)r["id"],
					Name = r["title"]?.ToString(),
					Large_img_url = r["image"]?.ToString(),
					Img_url = r["image"]?.ToString(),
					Total_time = r["readyInMinutes"]?.ToObject<int?>() ?? 0, // Nullable int with fallback
					Cook_time = r["cookingMinutes"]?.ToObject<int?>() ?? 0, // Nullable int with fallback
					Prep_time = r["preparationMinutes"]?.ToObject<int?>() ?? 0, // Nullable int with fallback
					Num_people_served = r["servings"]?.ToObject<int?>() ?? 1, // Nullable int with fallback
					isSaved = false
				}).ToList();

				/*
				for(int i = 0; i < recipes.Count; i++)
				{
					Recipe recipe = recipes[i];
					System.Diagnostics.Debug.WriteLine($"Recipe: {recipe.Name}, Time: {recipe.Total_time} min, Serves: {recipe.Num_people_served}");
					Boolean result = await GetIngredientsAsync(recipe);
					System.Diagnostics.Debug.WriteLine($"{recipe.Ingredients_List.Count} Ingredients done");
				}
				*/

				return recipes;
			}

			return null;
		}
	

	public async Task<Boolean> GetIngredientsAsync(Recipe rec)
        {
			int id = rec.Id;
            System.Diagnostics.Debug.WriteLine($"before call {id}");
            var url = $"{BaseUrl}recipes/{id}/information?apiKey={ApiKey}";
            System.Diagnostics.Debug.WriteLine($"{url}");

            var response = await client.GetAsync(url);
            Debug.WriteLine($"TEST API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");

            if (response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"success");

                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

				Debug.WriteLine(json.GetType());
				Debug.WriteLine(json["extendedIngredients"].GetType());
				JArray jsonList = (JArray)json["extendedIngredients"];

                List <Ingredient> ing = new List<Ingredient>();

				if (jsonList != null)
				{
                    Debug.WriteLine($"GET TYPE {jsonList.GetType()}");

                    foreach (JObject r in jsonList)
					{
						
						
						ing.Add(new Ingredient(
							r["originalName"]?.ToString(),
							r["measures"][STANDARD_UNITS]["amount"]?.ToObject<double?>() ?? 0,
							r["measures"][STANDARD_UNITS]["unitShort"]?.ToString(),
							r["image"]?.ToString(),
							r["original"]?.ToString()));
					}
				}
				rec.Ingredients_List.AddRange(ing);
				Debug.WriteLine("INGREDIENTS COUNT "+rec.Ingredients_List.Count.ToString());
				return true;
            }
			return false;
        }

    }
}

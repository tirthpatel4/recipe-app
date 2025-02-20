using Newtonsoft.Json.Linq;
using RecipeApp2025.Resources.Classes;
using System.Diagnostics;

namespace RecipeApp2025.Services
{
	public class RecipeService
	{
		private const string ApiKey = "b58ecd4c96354452b244c80df3e5da60";
		private const string BaseUrl = "https://api.spoonacular.com/";
		HttpClient client;

		public RecipeService()
		{
			client = new HttpClient();
		}

		public async Task<List<Recipe>> GetRecipesAsync(int pageNumber)
		{
			var url = $"{BaseUrl}recipes/complexSearch?apiKey={ApiKey}&addRecipeInformation=true&page={pageNumber}";
			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
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

				return recipes;
			}

			return null;
		}
	}
}

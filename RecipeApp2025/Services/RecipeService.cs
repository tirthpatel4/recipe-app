using System;
using Newtonsoft.Json.Linq;
using RecipeApp2025.Resources.Classes;
using System.Diagnostics;
using Syncfusion.Maui.Toolkit.PullToRefresh;

namespace RecipeApp2025.Services
{
	public class RecipeService
    {
        //private const string ApiKey = "60bd29f7cfc54795868a9a053cb447a3";
        private const string ApiKey = "e9086f66a8184e88a43bac38c112dba0";


        private const string BaseUrl = "https://api.spoonacular.com/";
		private const string STANDARD_UNITS = "us";
		HttpClient client;

		
		public RecipeService()
		{
			client = new HttpClient();
		}

        public async Task<List<Recipe>> GetRecipesAsync(string keyword, int pageNumber)
        {
            var url = $"{BaseUrl}recipes/complexSearch?apiKey={ApiKey}&addRecipeInformation=true&sort=random&offset={10 * pageNumber}";
            if (keyword != String.Empty)
            {
                Debug.WriteLine("keyword; no filter");
                url = $"{BaseUrl}recipes/complexSearch?apiKey={ApiKey}&addRecipeInformation=true&titleMatch={keyword}&sort=popularity&offset={10 * pageNumber}";
            }
            Debug.WriteLine($"serving:{App.CurrentFilter.MinServing}");
            if (App.CurrentFilter.MinServing != -1 ||  App.CurrentFilter.MaxServing != -1 || App.CurrentFilter.MaxPrep != -1 || App.CurrentFilter.Ingredients.Count > 0)
            {
                string inquiry = App.CurrentFilter.GetSearchInquiry();
                if (keyword != String.Empty)
                {
                    Debug.WriteLine("keyword; filter added");
                    url = $"{BaseUrl}recipes/complexSearch?apiKey={ApiKey}&addRecipeInformation=true&titleMatch={keyword}&sort=popularity&offset={10 * pageNumber}" + inquiry;
                }
                else
                {
                    Debug.WriteLine("no keyword; filter added");
                    url = $"{BaseUrl}recipes/complexSearch?apiKey={ApiKey}&addRecipeInformation=true&sort=popularity&offset={10 * pageNumber}" + inquiry;
                }
            }
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);
                Debug.WriteLine(json);
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


        public async Task<Boolean> GetIngredientsAsync(Recipe rec)
        {
			int id = rec.Id;
            //System.Diagnostics.Debug.WriteLine($"before call {id}");
            var url = $"{BaseUrl}recipes/{id}/information?apiKey={ApiKey}";
            //System.Diagnostics.Debug.WriteLine($"{url}");

            //Debug.Write("HELLO WORLD\n");
            var response = await client.GetAsync(url);
            //Debug.WriteLine($"TEST API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");

            if (response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"success");

                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

				//Debug.WriteLine(json.GetType());
				//Debug.WriteLine(json["extendedIngredients"].GetType());
				JArray jsonList = (JArray)json["extendedIngredients"];

                List <Ingredient> ing = new List<Ingredient>();

				if (jsonList != null)
				{
                   // Debug.WriteLine($"GET TYPE {jsonList.GetType()}");

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
				//Debug.WriteLine("INGREDIENTS COUNT "+rec.Ingredients_List.Count.ToString());
				return true;
            }
			return false;
        }

    
        /* pulls in steps data from api */
    public async Task<Boolean> GetStepsAsync(Recipe rec)
        {
            int id = rec.Id;
            //System.Diagnostics.Debug.WriteLine($"before call {id}");
            var url = $"{BaseUrl}recipes/{id}/analyzedInstructions?apiKey={ApiKey}";
            //System.Diagnostics.Debug.WriteLine($"{url}");

            //Debug.Write("HELLO WORLD\n");
            var response = await client.GetAsync(url);
            /*prints out result of api access */ 
            //Debug.WriteLine($"TEST API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");

            if (response.IsSuccessStatusCode)
            {
                //System.Diagnostics.Debug.WriteLine($"success");

                var content = await response.Content.ReadAsStringAsync();
                var json = JToken.Parse(content);

                Debug.WriteLine(json.GetType());
                Debug.WriteLine(json[0].GetType());
                Debug.WriteLine(json[0]["steps"].GetType());
                List<String> steps = new List<String>();

                /* json is a Jarray of Jobjects, each containing a Jarray. THis is because recipes might need to make multiple components, like a sauce */ 
                foreach (JObject o in json)
                {
                    string name = (String)o["name"];
                    if(!(name == null || name.Length == 0))
                    {
                        name = "- " + name + " -";
                        steps.Add(name);
                    }

                    foreach (JObject s in o["steps"])
                    {
                        steps.Add(s["number"] + ")   " + s["step"]);
                    }


                }
                rec.Steps_List.AddRange(steps);
                //Debug.WriteLine("STEPS COUNT " + rec.Steps_List.Count.ToString());
                return true;
            }
            return false;
        }

    }


}

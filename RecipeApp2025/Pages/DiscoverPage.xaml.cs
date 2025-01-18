using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;

namespace RecipeApp2025.Pages;

public partial class DiscoverPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
	private const string ApiKey = "DM QUINTON";
	private const string BaseUrl = "https://api.spoonacular.com/";
    public DiscoverPage()
	{
		InitializeComponent();
	}

    private async Task<List<string>> GetRecipesAsync()
    {
        var url = $"{BaseUrl}recipes/complexSearch?apiKey={ApiKey}";
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);
            var titles = json["results"].Select(r => r["title"].ToString()).ToList();

            // DEBUG ONLY!
            //foreach (string t in titles)
            //{
            //    Debug.Write(t);
            //}
            //Debug.WriteLine("");

            return titles;
        }
        else {
            Debug.WriteLine("Error: " + response.StatusCode);
            return null;
        }
    }

}
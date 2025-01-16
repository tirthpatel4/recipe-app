using System.Diagnostics;

namespace RecipeApp2025.Pages;

public partial class DiscoverPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
	private const string ApiKey = "REQUEST API KEY FROM QUINTON! DM VIA DISCORD.";
	private const string BaseUrl = "https://api.spoonacular.com/";
    public DiscoverPage()
	{
		InitializeComponent();
	}

    private async Task<string> GetRecipesAsync()
    {
        var url = $"{BaseUrl}recipes/complexSearch?apiKey={ApiKey}";
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        else {
            Debug.WriteLine("Error: " + response.StatusCode);
            return null;
        }
    }

}
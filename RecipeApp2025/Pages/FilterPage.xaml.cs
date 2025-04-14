namespace RecipeApp2025.Pages;

public partial class FilterPage : ContentPage
{


	public FilterPage()
	{
		InitializeComponent();
	}

	// for the apply button 
	private async void ApplyFilter(object sender, EventArgs e)
	{
		string Filter = "";

		if (PopularityPicker.SelectedItem != null)
		{

			Filter = PopularityPicker.SelectedItem.ToString();

		}

		await Shell.Current.GoToAsync($"/DiscoverPage?popularity={Filter}");
	}
}
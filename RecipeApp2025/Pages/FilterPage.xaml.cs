using System.Diagnostics;

namespace RecipeApp2025.Pages;

public partial class FilterPage : ContentPage
{
	private int _minServing = -1;
	private int _maxServing = -1;
	private int _maxPrep = -1;
	public FilterPage()
	{
		InitializeComponent();

		for (int i = 0; i < 2; i++)
		{
			App.CurrentFilter.Ingredients.Add("none");
            App.CurrentFilter.Ingredients.Add("none");
        }
	}
	protected override void OnAppearing()
	{
		cb1.IsChecked = Preferences.Get("cb1", false);
        cb2.IsChecked = Preferences.Get("cb2", false);
        cb3.IsChecked = Preferences.Get("cb3", false);

		e1.Text = Preferences.Get("e1", string.Empty);
        e2.Text = Preferences.Get("e2", string.Empty);
        e3.Text = Preferences.Get("e3", string.Empty);

		p1.SelectedIndex = Preferences.Get("p1", 0);
        p2.SelectedIndex = Preferences.Get("p2", 0);
		p3.SelectedIndex = Preferences.Get("p3", 0);
    }
    public async void OnEntryChanged(object sender, TextChangedEventArgs e)
	{
		var entry =  sender as Entry;
		int value;
		bool success = int.TryParse(e.NewTextValue, out value);
        if (entry != null && success)
		{
            Preferences.Set($"e{entry.AutomationId}", e.NewTextValue);
            switch (entry.AutomationId)
			{
				case "1":
                    _minServing = value;
					if (cb1.IsChecked)
					{
                        App.CurrentFilter.MinServing = _minServing;
                    }
                    break;
				case "2":
                    _maxServing = value;
                    if (cb2.IsChecked)
                    {
                        App.CurrentFilter.MaxServing = _maxServing;
                    }
                    break;
				case "3":
                    _maxPrep = value;
                    if (cb3.IsChecked)
                    {
                        App.CurrentFilter.MaxPrep = _maxPrep;
                    }
                    break;
				default:
					break;
			}
		}
	}
	public async void OnCheckboxChanged(object sender, CheckedChangedEventArgs e)
	{
		var check = sender as CheckBox;
		if (check != null)
		{
            Preferences.Set($"cb{check.AutomationId}", e.Value);
            switch (check.AutomationId)
			{
				case "1":
					if (e.Value)
					{
                        App.CurrentFilter.MinServing = _minServing;
					}
					else
					{
                        App.CurrentFilter.MinServing = -1;
					}
					break;
				case "2":
                    if (e.Value)
                    {
                        App.CurrentFilter.MaxServing = _maxServing;
                    }
                    else
                    {
                        App.CurrentFilter.MaxServing = -1;
                    }
                    break;
				case "3":
                    if (e.Value)
                    {
                        App.CurrentFilter.MaxPrep = _maxPrep;
                    }
                    else
                    {
                        App.CurrentFilter.MaxPrep = -1;
                    }
                    break;
				default:
					break;
			}
		}
	}
	public async void OnPickerChanged(object sender, EventArgs e)
	{
		var picker = sender as Picker;
		int id = int.Parse(picker.AutomationId);
		if (id != 3)
		{
			Debug.WriteLine("ID: " + id);
            App.CurrentFilter.Ingredients[id] = picker.SelectedItem.ToString();
        }
		else
		{
			App.CurrentFilter.Sort = picker.SelectedItem.ToString().ToLower();
		}
		Preferences.Set($"p{picker.AutomationId}", picker.SelectedIndex);
	}
	public async void ApplyFilter(object sender, EventArgs e)
	{

	}
}
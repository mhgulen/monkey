namespace MonkeyFinder.ViewModel;

[QueryProperty(nameof(Dersler), "Dersler")]
public partial class DersViewModel : BaseViewModel
{
    IMap map;
    public DersViewModel(IMap map)
    {
        this.map = map;
    }

    [ObservableProperty]
    Dersler dersler;

    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(Dersler.Latitude, Dersler.Longitude, new MapLaunchOptions
            {
                Name = Dersler.Name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }
}

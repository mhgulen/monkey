using Microsoft.Maui.Networking;
using MonkeyFinder.Services;
using System.Data.SqlTypes;

namespace MonkeyFinder.ViewModel;

[QueryProperty(nameof(Monkey), "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    bool isRefreshing;
    [ObservableProperty]
    Monkey monkey;
    public ObservableCollection<Dersler> Derslers { get; } = new();
    IMap map;
    MonkeyService monkeyService;
    IConnectivity connectivity;
    public MonkeyDetailsViewModel(IMap map, MonkeyService monkeyService, IConnectivity connectivity)
    {
        this.map = map;
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
    }
    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(Monkey.Latitude, Monkey.Longitude, new MapLaunchOptions
            {
                Name = Monkey.Name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task GetDerslers()
    {
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }
            string ss =monkey.Name;
            IsBusy = true;
            var derslers = await monkeyService.GetDerslers();

            if (Derslers.Count != 0)
                Derslers.Clear();

            foreach (var ders in derslers)
                Derslers.Add(ders);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get ders: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToDetails(Dersler dersler)
    {
        if (dersler == null)
            return;

        await Shell.Current.GoToAsync(nameof(DersPage), true, new Dictionary<string, object>
        {
            {"Dersler", dersler }
        });
    }
}

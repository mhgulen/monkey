using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    HttpClient httpClient;
    public MonkeyService()
    {
        this.httpClient = new HttpClient();
    }

    List<Monkey> monkeyList;
    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeyList?.Count > 0)
            return monkeyList;

        // Online
        //var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
        //if (response.IsSuccessStatusCode)
        //{
        //    monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        //}
        // Offline
        using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents);

        return monkeyList;
    }
    List<Dersler> derslerList;
    public async Task<List<Dersler>> GetDerslers()
    {
        if (derslerList?.Count > 0)
            return derslerList;

        // Online
        //var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
        //if (response.IsSuccessStatusCode)
        //{
        //    monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        //}
        // Offline
        using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydataDersler.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        derslerList = JsonSerializer.Deserialize<List<Dersler>>(contents);

        return derslerList;
    }
}

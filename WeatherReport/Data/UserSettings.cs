using System.IO;
using System.IO.IsolatedStorage;
using System.Text.Json;
using WeatherReport.WinApp.Interfaces;

namespace WeatherReport.Data;

public class UserSettings : IUserSettings
{
    private const string SettingsFileName = "WeatherReportSettings.json";

    public string? SelectedCountry { get; set; }
    public string? SelectedCity { get; set; }

    public async Task Save()
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(SettingsFileName, FileMode.Create, isoStore))
        {
            await JsonSerializer.SerializeAsync(isoStream, this);
        }
    }

    public async Task Load()
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        if (isoStore.FileExists(SettingsFileName))
        {
            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(SettingsFileName, FileMode.Open, isoStore))
            {
                UserSettings? loaded = await JsonSerializer.DeserializeAsync<UserSettings>(isoStream);
                if(loaded != null)
                {
                    SelectedCountry = loaded.SelectedCountry;
                    SelectedCity = loaded.SelectedCity;
                }
            }
        }
    }
}

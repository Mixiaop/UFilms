using U.Settings;

namespace UFilm.Configuration
{
    [USettingsPathArribute("DatabaseSettings.json", "/Config/UFilm/")]
    public class DatabaseSettings : USettings<DatabaseSettings>
    {
        public string SqlConnectionString { get; set; }
    }
}

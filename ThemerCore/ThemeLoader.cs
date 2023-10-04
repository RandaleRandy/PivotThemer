

using System.Runtime.Serialization;
using System.Text.Json;
using ThemerCore;

public class ThemeLoader : IThemeLoader{


    public Dictionary<string, string> GetThemeMapping(string themeName)
    {
        themeName = themeName.ToLower();
        var themeMappingText = File.ReadAllText($"./Themes/${themeName}.pivotthemer.json");
        var themeMapping = JsonSerializer.Deserialize<Dictionary<string, string>>(themeMappingText);

        if (themeMapping == null)
            throw new Exception($"Themefile did not parse. Check ${themeName}.pivotthemer.json");

        return themeMapping;
    }

}

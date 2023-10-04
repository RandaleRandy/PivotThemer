

using System.Runtime.Serialization;
using System.Text.Json;
using ThemerCore;

public class ThemeLoader : IThemeLoader{


    public ThemeModel GetTheme(string themeName)
    {
        themeName = themeName.ToLower();
        var filepath = Path.GetFullPath($"./Themes/{themeName}.pivotthemer.json");
        var themeMappingText = File.ReadAllText(filepath);
        var themeMapping = JsonSerializer.Deserialize<ThemeModel>(themeMappingText);

        if (themeMapping == null)
            throw new Exception($"Themefile did not parse. Check {themeName}.pivotthemer.json");

        return themeMapping;
    }

}

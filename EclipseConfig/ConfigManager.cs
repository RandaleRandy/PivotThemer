using System.Text.Json;
using System.Globalization;
using ThemerCore;
using System.Drawing;

namespace EclipsePrefsReader;

public class ConfigManager
{
    private readonly PrefReader _reader;
    private readonly PrefWriter _writer;
    private readonly List<ThemeToEclipseMapping> _themeToEclipseMappings;
    private readonly IThemeLoader _loader;
    
    public ConfigManager(string absolutePath, IThemeLoader themeLoader)
    {
        _reader = new(absolutePath);
        _writer = new(absolutePath);
        _loader = themeLoader;
        var mappingContent = File.ReadAllText("./Configuration/ThemeToPrefsMapping.json");
        _themeToEclipseMappings = JsonSerializer.Deserialize<List<ThemeToEclipseMapping>>(mappingContent) ?? new List<ThemeToEclipseMapping>();
    }
    public void UpdateTheme(string themeName, bool appendNonExistant = true)
    {
        var themeMapping = _loader.GetTheme(themeName);

        if (_themeToEclipseMappings.Count == 0) 
            throw new FileNotFoundException("No mappings found");
        var prefs = _reader.Read();
        foreach (var themeEclipseMapping in _themeToEclipseMappings)
        {
            string themeValueHex;
            var propertyInfo = typeof(ThemeModel).GetProperty(themeEclipseMapping.themeIdentifier);
            if(propertyInfo == null)
                continue;
            themeValueHex = propertyInfo.GetValue(themeMapping)!.ToString()!;
            if(string.IsNullOrEmpty(themeValueHex)){
                continue;
            }
            var themeValueRgb = ConvertHexToRgbString(themeValueHex);

            var eclipseValueExists = prefs.ContainsKey(themeEclipseMapping.eclipseIdentifier);
            if (!eclipseValueExists && appendNonExistant)
                prefs.Add(themeEclipseMapping.eclipseIdentifier, themeValueRgb);
            if (eclipseValueExists)
            {
                prefs[themeEclipseMapping.eclipseIdentifier] = themeValueRgb;
            }
        }
        _writer.Write(prefs);
    }

    private static string ConvertHexToRgbString(string themeValueHex)
    {
        var color = ColorTranslator.FromHtml(themeValueHex);
        return $"{color.R},{color.G},{color.B}";

    }

    // public void TintNotDefined(string rgbString, string themeName)
    // {
    //     var rgbValueRegex = new Regex(@"[0-9]{1,3}\,[0-9]{1,3}\,[0-9]{1,3}", RegexOptions.Compiled);
    //     var prefs = _reader.Read();
    //     foreach (var pref in prefs)
    //     {
    //         var eclipseMapping = _themeToEclipseMappings.FirstOrDefault(x => x.eclipseIdentifier == pref.Key);
    //         if (eclipseMapping != null)
    //         {
    //             if (theme.Exists(x => x.Identifier == eclipseMapping.themeIdentifier))
    //             {
    //                 continue;
    //             }
    //         }
    //         if (rgbValueRegex.IsMatch(pref.Value))
    //             prefs[pref.Key] = rgbString;
    //     }
    //     _writer.Write(prefs);
    // }
}
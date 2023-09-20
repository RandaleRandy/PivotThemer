using System.Text.Json;
using System.Text.RegularExpressions;

namespace EclipsePrefsReader;

public class ConfigManager
{
    private readonly PrefReader _reader;
    private readonly PrefWriter _writer;
    private readonly List<ThemeToEclipseMapping> _themeToEclipseMappings;
    
    public ConfigManager(string absolutePath)
    {
        _reader = new(absolutePath);
        _writer = new(absolutePath);
        var mappingContent = File.ReadAllText("./Configuration/ThemeToPrefsMapping.json");
        _themeToEclipseMappings = JsonSerializer.Deserialize<List<ThemeToEclipseMapping>>(mappingContent) ?? new List<ThemeToEclipseMapping>();
    }
    public void UpdateTheme(List<ThemeModel> theme, bool appendNonExistant = true)
    {
        if (_themeToEclipseMappings.Count == 0) throw new Exception("No mappings found");
        var prefs = _reader.Read();
        foreach (var themeEclipseMapping in _themeToEclipseMappings)
        {
            var themeItem = theme.FirstOrDefault(x => x.Identifier == themeEclipseMapping.themeIdentifier);
            var eclipseValueExists = prefs.ContainsKey(themeEclipseMapping.eclipseIdentifier);
            if (themeItem != null && !eclipseValueExists && appendNonExistant)
                prefs.Add(themeEclipseMapping.eclipseIdentifier, themeItem.Rgb);
            if (themeItem != null && eclipseValueExists)
            {
                prefs[themeEclipseMapping.eclipseIdentifier] = themeItem.Rgb;
            }
        }
        
        _writer.Write(prefs);
    }

    public void TintNotDefined(string rgbString, List<ThemeModel> theme)
    {
        var rgbValueRegex = new Regex(@"[0-9]{1,3}\,[0-9]{1,3}\,[0-9]{1,3}", RegexOptions.Compiled);
        var prefs = _reader.Read();
        foreach (var pref in prefs)
        {
            var eclipseMapping = _themeToEclipseMappings.FirstOrDefault(x => x.eclipseIdentifier == pref.Key);
            if (eclipseMapping != null)
            {
                if (theme.Exists(x => x.Identifier == eclipseMapping.themeIdentifier))
                {
                    continue;
                }
            }
            
            if (rgbValueRegex.IsMatch(pref.Value))
                prefs[pref.Key] = rgbString;
        }
        _writer.Write(prefs);
    }
}
using System.Text.Json;

namespace EclipsePrefsReader;

public class PrefWriter
{
    private readonly string _absolutePath;
    
    public PrefWriter(string absolutePath)
    {
        _absolutePath = absolutePath;
    }
    public void Write(Dictionary<string, string> prefs)
    {
        var lines = new List<string>();
        foreach (var (key, value) in prefs)
        {
            lines.Add($"{key}={value}");
        }
        File.WriteAllLines(_absolutePath, lines);
    }
    public void WriteToConfigurationFolder(List<ThemeToEclipseMapping> prefs){
        var prefsSerialized = JsonSerializer.Serialize(prefs, options: new(){ WriteIndented = true });
        var filepath = Path.GetFullPath("./Configuration/unconfigured.eclipse.json");
        File.WriteAllText(filepath, prefsSerialized);
    }
}
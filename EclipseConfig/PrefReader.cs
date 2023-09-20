namespace EclipsePrefsReader;

public class PrefReader
{
    private readonly string _absolutePath;
    public PrefReader(string absolutePath)
    {   
        _absolutePath = absolutePath;
    }
    public Dictionary<string, string> Read()
    {
        var prefs = new Dictionary<string, string>();
        var lines = File.ReadAllLines(_absolutePath);
        foreach (var line in lines)
        {
            var parts = line.Split('=');
            if (parts.Length == 2)
            {
                prefs.Add(parts[0], parts[1]);
            }
        }
        return prefs;
    } 
}
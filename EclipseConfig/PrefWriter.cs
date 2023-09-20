﻿namespace EclipsePrefsReader;

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
}
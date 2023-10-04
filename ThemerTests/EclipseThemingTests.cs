using EclipsePrefsReader;
using System.Text.Json;
using ThemerTests.Configuration;

namespace ThemerTests;

public class Tests
{
    private ConfigManager? cut;
    [SetUp]
    public void Setup()
    {
        // read the configuration file in ./Configuration
        var locationsText = File.ReadAllText("./Configuration/Locations.json");
        var locations = JsonSerializer.Deserialize<List<Locations>>(locationsText);
        if ( locations == null) 
            throw new FileNotFoundException();
            
        if(Environment.OSVersion.Platform == PlatformID.Win32NT)
            cut = new ConfigManager( locations.Single( x => x.PlatformID == PlatformID.Win32NT.ToString()).EclipseWorkbenchPath );
        else if(Environment.OSVersion.Platform == PlatformID.Unix)
            cut = new ConfigManager( locations.Single( x => x.PlatformID == PlatformID.Unix.ToString()).EclipseWorkbenchPath);
        else if(Environment.OSVersion.Platform == PlatformID.Other)
            cut = new ConfigManager( locations.Single( x => x.PlatformID == PlatformID.Other.ToString()).EclipseWorkbenchPath);
        else
            throw new Exception("OS not supported"
            );
    }

    /// <summary>
    /// Tests the activation of a theme by updating the theme models.
    /// </summary>
    [Test]
    public void ActivateTheme()
    {
        try
        {
            // cut!.UpdateTheme(_themeModels);
        }
        catch
        {
            Assert.Fail();
        }
    }
    [Test]
    public void TintNotDefined()
    {
        try
        {
            // cut!.TintNotDefined("255,0,255", _themeModels);
        }
        catch
        {
            Assert.Fail();
        }
    }
}
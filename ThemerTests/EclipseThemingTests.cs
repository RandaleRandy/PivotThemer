using EclipsePrefsReader;
using System.Security.AccessControl;
using System.Text.Json;
using ThemerTests.Configuration;

namespace ThemerTests;

public class Tests
{
    private ConfigManager? cut;

    [SetUp]
    public void Setup()
    {        
        
        var locationsText = File.ReadAllText("./Configuration/Locations.json");
        var locations = JsonSerializer.Deserialize<List<Locations>>(locationsText) ?? throw new FileNotFoundException();

        var themeLoader = new ThemeLoader();

        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            cut = new ConfigManager( locations.Single( x => x.PlatformID == PlatformID.Win32NT.ToString()).EclipseWorkbenchPath, themeLoader );
        else if(Environment.OSVersion.Platform == PlatformID.Unix)
            cut = new ConfigManager( locations.Single( x => x.PlatformID == PlatformID.Unix.ToString()).EclipseWorkbenchPath, themeLoader);
        else if(Environment.OSVersion.Platform == PlatformID.Other)
            cut = new ConfigManager( locations.Single( x => x.PlatformID == PlatformID.Other.ToString()).EclipseWorkbenchPath, themeLoader);
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
            cut!.UpdateTheme("Catppuccin");
            // cut!.UpdateTheme(_themeModels);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
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
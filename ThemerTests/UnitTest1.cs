using EclipsePrefsReader;
using System.Text.Json;

namespace ThemerTests;

public class Tests
{
    private ConfigManager cut;
    private List<ThemeModel> _themeModels;
    [SetUp]
    public void Setup()
    {
        // cut = new ConfigManager(
        //     "C:\\Users\\Stefan\\RiderProjects\\EclipseThemer\\EclipseThemer\\Testdata\\org.eclipse.ui.workbench.prefs");
        // decide what config to pull dependant on the operating system
        if(Environment.OSVersion.Platform == PlatformID.Win32NT)
            cut = new ConfigManager(
                "C:\\Users\\Stefan\\eclipse-workspace\\.metadata\\.plugins\\org.eclipse.core.runtime\\.settings\\org.eclipse.ui.workbench.prefs");
        else if(Environment.OSVersion.Platform == PlatformID.Unix)
            cut = new ConfigManager(
                "/home/stefan/.config/eclipse/org.eclipse.platform_4.21.0_155965261_linux_gtk_x86_64/configuration/.settings/org.eclipse.ui.workbench.prefs");
        else
            throw new Exception("OS not supported"
            );
        _themeModels = JsonSerializer.Deserialize<List<ThemeModel>>(File.ReadAllText("C:\\Users\\Stefan\\RiderProjects\\EclipseThemer\\EclipseConfig\\Configuration\\Catppuccin.json")) ?? new();
    }

    [Test]
    public void ActivateTheme()
    {
        try
        {
            cut.UpdateTheme(_themeModels);
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
            cut.TintNotDefined("255,0,255", _themeModels);
        }
        catch
        {
            Assert.Fail();
        }
    }
}
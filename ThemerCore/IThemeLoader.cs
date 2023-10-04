namespace ThemerCore;
public interface IThemeLoader
{
    public Dictionary<string, string> GetThemeMapping(string themeName);
}


public class ThemeLoader{

    private string _themeName;
    private string _theme;


    public ThemeLoader(){

    }

    public void LoadTheme(string themeName){
        _theme = File.ReadAllText($"./Themes/{themeName}.json");
    }

}
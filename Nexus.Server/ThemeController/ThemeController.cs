namespace Nexus.Server.ThemeController
{
    public class ThemeControllerX
    {
        private static bool DarkMode = false;

        public ThemeControllerX() { 
        }

        public bool ToggleThemeMode(bool mode)
        {
            DarkMode = mode;
            Console.WriteLine(DarkMode + " dark?");
            return DarkMode;
        }

        public bool GetTheme()
        {
            Console.WriteLine(DarkMode + " get?");
            return DarkMode;
        }
    }
}

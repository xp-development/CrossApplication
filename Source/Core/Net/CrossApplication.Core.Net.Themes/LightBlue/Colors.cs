using System.Windows.Media;

namespace CrossApplication.Core.Net.Themes.LightBlue
{
    // ReSharper disable PossibleNullReferenceException
    public static class Colors
    {
        public static readonly Color TextColor = (Color) new ColorConverter().ConvertFrom(Core.Themes.LightBlue.Colors.Text);
        public static readonly Color HintTextColor = (Color) new ColorConverter().ConvertFrom(Core.Themes.LightBlue.Colors.HintText);
        public static readonly Color NavigationBarBackgroundColor = (Color) new ColorConverter().ConvertFrom(Core.Themes.LightBlue.Colors.NavigationBarBackground);
    }
}
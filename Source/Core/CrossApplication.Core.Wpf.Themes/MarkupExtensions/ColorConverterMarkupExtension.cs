using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace CrossApplication.Core.Wpf.Themes.MarkupExtensions
{
    public class ColorConverterMarkupExtension : MarkupExtension
    {
        public ColorConverterMarkupExtension(System.Drawing.Color color)
        {
            _color = color;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Color.FromRgb(_color.R, _color.G, _color.B);
        }

        private readonly System.Drawing.Color _color;
    }
}
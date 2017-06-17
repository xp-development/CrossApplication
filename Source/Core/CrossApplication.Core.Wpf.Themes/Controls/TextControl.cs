using System.Windows;
using System.Windows.Controls;

namespace CrossApplication.Core.Wpf.Themes.Controls
{
    public class TextControl : ContentControl
    {
        public string LabelText
        {
            get => (string) GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public Dock LabelAlignment
        {
            get => (Dock) GetValue(LabelAlignmentProperty);
            set => SetValue(LabelAlignmentProperty, value);
        }

        public FontWeight LabelFontWeight
        {
            get => (FontWeight) GetValue(LabelFontWeightProperty);
            set => SetValue(LabelFontWeightProperty, value);
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(TextControl), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty LabelAlignmentProperty = DependencyProperty.Register("LabelAlignment", typeof(Dock), typeof(TextControl), new PropertyMetadata(default(Dock)));
        public static readonly DependencyProperty LabelFontWeightProperty = DependencyProperty.Register("LabelFontWeight", typeof(FontWeight), typeof(TextControl), new PropertyMetadata(default(FontWeight)));
    }
}
using System;
using System.Windows.Markup;
using CrossApplication.Core.Common;

namespace CrossApplication.Wpf.Common
{
    [MarkupExtensionReturnType(typeof(string))]
    public class LocalizationExtension : MarkupExtension
    {
        public Type ResourceType { get; set; }
        public string Name { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ResourceType == null || string.IsNullOrWhiteSpace(Name))
                return "";

            return LocalizationManager.Get(ResourceType, Name);
        }
    }
}
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Markup;

namespace WpfSimpleNavi
{
    public class CustomDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is null) return base.SelectTemplate(item, container);

            var itemType = item.GetType();
            if (itemType.Name.EndsWith("ViewModel") == false) return base.SelectTemplate(item, container);

            var appResouces = Application.Current.Resources;
            if (appResouces.Contains(itemType.Name) == false)
            {
                var types = Assembly.GetEntryAssembly().GetTypes();
                var targetType = types.FirstOrDefault(x => x.Name == itemType.Name.Substring(0,itemType.Name.Length - 5));
                if (targetType is null) return base.SelectTemplate(item, container);
                var dataTemplate = CreateTemplate(itemType, targetType);
                appResouces.Add(itemType.Name, dataTemplate);
            }

            var template = Application.Current.FindResource(itemType.Name);
            return template as DataTemplate;
        }

        private object CreateTemplate(Type viewModelType, Type viewType)
        {
            var context = new ParserContext();
            const string viewxmlnms = "v";
            const string vmxmlns = "vm";
            context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
            context.XamlTypeMapper.AddMappingProcessingInstruction(viewxmlnms, viewType.Namespace, viewType.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction(vmxmlns, viewModelType.Namespace, viewModelType.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add(viewxmlnms, viewxmlnms);
            context.XmlnsDictionary.Add(vmxmlns, vmxmlns);

            var xamlDataTemplate =
            $@"<DataTemplate DataType=""{{x:Type {vmxmlns}:{viewModelType.Name}}}"">
                <{viewxmlnms}:{viewType.Name} />
            </DataTemplate>";

            var template = XamlReader.Parse(xamlDataTemplate, context);
            return template;
        }
    }
}

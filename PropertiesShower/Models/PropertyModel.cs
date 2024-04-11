using Autodesk.Revit.DB;
using System.Collections.ObjectModel;
using System.Linq;

namespace PropertiesShower.Models
{
    internal class PropertyModel
    {
        public ObservableCollection<PropertyVM> Properties { get; }
        public PropertyModel(Element selectedElement)
        {
            Properties = new ObservableCollection<PropertyVM>(GetElementProperties(selectedElement));
        }
        private ObservableCollection<PropertyVM> GetElementProperties(Element element)
        {
            var properties = new ObservableCollection<PropertyVM>();
            foreach (var p in element.Parameters.Cast<Parameter>())
            {
                var parameterValue = GetPropertyValue(p);
                if (parameterValue != null)
                {
                    properties.Add(new PropertyVM(p.Definition.Name, parameterValue));
                }
            }

            return properties;
        }
        private string GetPropertyValue(Parameter parameter)
        {
            switch (parameter.StorageType)
            {
                case StorageType.Double:
                    return parameter.AsDouble().ToString();
                case StorageType.Integer:
                    return parameter.AsInteger().ToString();
                case StorageType.String:
                    return parameter.AsString();
                case StorageType.ElementId:
                    return parameter.AsElementId().IntegerValue.ToString();
                default:
                    return "<Unsupported Type>";
            }
        }
    }
}

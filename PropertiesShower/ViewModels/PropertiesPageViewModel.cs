using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using PropertiesShower.Models;

namespace PropertiesShower.ViewModels
{
    internal class PropertiesPageViewModel
    {
        public ObservableCollection<PropertyVM> Properties { get; private set; }
        public PropertiesPageViewModel(Element selectedElement)
        {
            Properties = GetElementParameters(selectedElement);
        }

        private ObservableCollection<PropertyVM> GetElementParameters(Element element)
        {
            var properties = new ObservableCollection<PropertyVM>();
            foreach (var p in element.Parameters.Cast<Parameter>())
            {
                var parameterValue = GetParameterValue(p);
                if (parameterValue != null)
                {
                    properties.Add(new PropertyVM(p.Definition.Name, parameterValue));
                }
            }

            return properties;
        }
        private string GetParameterValue(Parameter parameter)
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

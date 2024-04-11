using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
using System.Text;

namespace PropertiesShower.Commands
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    internal class PropertiesShower : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var choices = uidoc.Selection;

            var hasPickOne = choices.PickObject(ObjectType.Element, "Выберите элемент");
            if (hasPickOne == null)
            {
                return Result.Succeeded;
            }

            var selectedElement = doc.GetElement(hasPickOne.ElementId);

            var listParameters = GetElementParameters(selectedElement);

            TaskDialog.Show(selectedElement.Name, listParameters);

            return Result.Succeeded;
        }

        private string GetElementParameters(Element element)
        {
            var stringBuilder = new StringBuilder();
            foreach (var p in element.Parameters.Cast<Parameter>())
            {
                var parameterValue = GetParameterValue(p);
                if (parameterValue != null)
                {
                    stringBuilder.AppendLine($"{p.Definition.Name}: {parameterValue}");
                }
            }

            return stringBuilder.ToString();
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

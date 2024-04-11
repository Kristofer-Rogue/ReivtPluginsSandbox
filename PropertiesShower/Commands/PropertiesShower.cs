using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
using System.Text;
using PropertiesShower.Views;
using PropertiesShower.ViewModels;

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

            var viewModel = new PropertiesViewModel(selectedElement);
            var view = new PropertiesView
            {
                DataContext = viewModel,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            view.ShowDialog();

            return Result.Succeeded;
        }
    }
}

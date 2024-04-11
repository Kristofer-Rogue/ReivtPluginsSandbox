using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using PropertiesShower.ViewModels;
using PropertiesShower.Views;

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

            var view = new PropertiesView();
            var viewModel = new PropertiesViewModel(selectedElement, uidoc, view);
            view.DataContext = viewModel;
            view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            view.Show();

            return Result.Succeeded;
        }
    }
}

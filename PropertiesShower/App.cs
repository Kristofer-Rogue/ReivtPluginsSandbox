using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PropertiesShower
{
    public class App : IExternalApplication
    {
        private const string СommandsNamespace = "PropertiesShower.Commands.";
        private const string TabName = "Revit Plugins Sandbox";
        private const string ImageFolderPath = "pack://application:,,,/PropertiesShower;component/Images/";
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            CreateRibbonTab(application);
            CreateRibbonPanel(application, "Picker");
            CreateButton(application, "propertiesShowerButton", "Показать свойства", "PropertiesShower", "picker_logo.ico");

            return Result.Succeeded;
        }

        private void CreateRibbonTab(UIControlledApplication application)
        {
            application.CreateRibbonTab(TabName);
        }
        private void CreateRibbonPanel(UIControlledApplication application, string panelName)
        {
            var sandboxRibbonPanel = application.CreateRibbonPanel(TabName, panelName);
        }
        private void CreateButton(UIControlledApplication application, string buttonName, string buttonText, string commandName, string imageName)
        {
            var assemblyName = Assembly.GetExecutingAssembly().Location;
            var buttonData = new PushButtonData(buttonName, buttonText,
                assemblyName, СommandsNamespace + commandName);
            var sandboxRibbonPanel = application.GetRibbonPanels(TabName).FirstOrDefault();
            var button = sandboxRibbonPanel?.AddItem(buttonData) as PushButton;

            if (button != null)
            {
                button.LargeImage = LoadImage(imageName);
            }
        }

        private ImageSource LoadImage(string imageName)
        {
            return new BitmapImage(new Uri(ImageFolderPath + imageName, UriKind.Absolute));
        }
    }
}

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using PropertiesShower.Models;
using PropertiesShower.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PropertiesShower.ViewModels
{
    internal class PropertiesViewModel : INotifyPropertyChanged
    {
        private UIDocument uidoc;
        private Window window;
        private Element selectedElement;
        public PropertiesViewModel(Element selectedElement, UIDocument uidoc, Window window)
        {
            this.uidoc = uidoc;
            this.window = window;
            PropertiesPage = new PropertiesPage();
            SelectedElement = selectedElement;
            propertiesPageViewModel = new PropertiesPageViewModel(new PropertyModel(selectedElement));
            PropertiesPage.DataContext = propertiesPageViewModel;
        }

        public Element SelectedElement
        {
            get => selectedElement;
            set
            {
                selectedElement = value;
                OnPropertyChanged();
                UpdatePropertiesPageViewModel();
            }
        }

        public PropertiesPage PropertiesPage { get; }
        private PropertiesPageViewModel propertiesPageViewModel;


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void UpdatePropertiesPageViewModel()
        {
            propertiesPageViewModel = new PropertiesPageViewModel(new PropertyModel(selectedElement));
            PropertiesPage.DataContext = propertiesPageViewModel;
        }
        public Models.RelayCommand SelectAnotherElementCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    var doc = uidoc.Document;
                    var choices = uidoc.Selection;
                    var hasPickOne = choices.PickObject(ObjectType.Element, "Выберите элемент");

                    if (hasPickOne == null)
                    {
                        return;
                    }

                    SelectedElement = doc.GetElement(hasPickOne);
                    window.WindowState = WindowState.Normal;
                    window.Focus();
                });


            }
        }
    }
}



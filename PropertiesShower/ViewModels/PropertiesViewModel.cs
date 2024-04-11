using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autodesk.Revit.DB;
using PropertiesShower.Views;

namespace PropertiesShower.ViewModels
{
    internal class PropertiesViewModel : INotifyPropertyChanged
    {
        public PropertiesViewModel(Element selectedElement)
        {
            SelectedElement = selectedElement;
            propertiesPageViewModel = new PropertiesPageViewModel(selectedElement);
            PropertiesPage = new PropertiesPage();
            PropertiesPage.DataContext = propertiesPageViewModel;
        }
        private Element selectedElement;
        public Element SelectedElement
        {
            get => selectedElement;
            set
            {
                selectedElement = value;
                OnPropertyChanged(nameof(SelectedElement));
            }
        }

        public PropertiesPage PropertiesPage { get; }
        private PropertiesPageViewModel propertiesPageViewModel;


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //public RelayCommand SelectAnotherElementCommand
    }
}


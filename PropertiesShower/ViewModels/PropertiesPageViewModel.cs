using PropertiesShower.Models;
using System.Collections.ObjectModel;

namespace PropertiesShower.ViewModels
{
    internal class PropertiesPageViewModel
    {
        public ObservableCollection<PropertyVM> Properties { get; private set; }
        public PropertiesPageViewModel(PropertyModel model)
        {
            Properties = model.Properties;
        }
    }
}

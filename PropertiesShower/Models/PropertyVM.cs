namespace PropertiesShower.Models
{
    internal class PropertyVM
    {
        public string Title { get; private set; }
        public string Value { get; private set; }

        public PropertyVM(string name, string value)
        {
            Title = name;
            Value = value;
        }
    }
}

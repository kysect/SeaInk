namespace SeaInk.Endpoints.Client.Pages.TableGeneration
{
    public partial class TableGenerationPage
    {
        private string _selectedTab;

        public TableGenerationPage()
        {
            _selectedTab = "group";
        }

        private void OnSelectedTabChanged(string name)
            => _selectedTab = name;
    }
}
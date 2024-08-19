using TaskPlanner.MVVM.Views;
using TaskPlanner.Services;
using Microsoft.Maui.Storage;

namespace TaskPlanner
{
    public partial class App : Application
    {
        private readonly DatabaseService _databaseService;

        public App()
        {
            InitializeComponent();

            //initialize the DatabaseService
            _databaseService = new DatabaseService();

            //check if the user has accepted the privacy agreement
            bool hasAcceptedPrivacy = Preferences.Get("HasAcceptedPrivacy", false);

            if (hasAcceptedPrivacy)
            {
                MainPage = new NavigationPage(new MainView(_databaseService));
            }
            else
            {
                MainPage = new NavigationPage(new PrivacyPrinciples(_databaseService));
            }
        }
    }
}

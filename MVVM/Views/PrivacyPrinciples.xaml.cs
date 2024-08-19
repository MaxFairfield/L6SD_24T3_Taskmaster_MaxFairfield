using TaskPlanner.Services;
using Microsoft.Maui.Storage; //for using Preferences

namespace TaskPlanner.MVVM.Views
{
    public partial class PrivacyPrinciples : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public PrivacyPrinciples(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnAcceptClicked(object sender, EventArgs e)
        {
            //save that the user has accepted the privacy agreement
            Preferences.Set("HasAcceptedPrivacy", true);

            //navigate to the main page with the DatabaseService
            await Navigation.PushAsync(new MainView(_databaseService));

            //remove this page from the navigation stack
            Navigation.RemovePage(this);
        }

        private void OnDeclineClicked(object sender, EventArgs e)
        {
#if ANDROID
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif IOS
            //iOS doesn't support killing the process
#endif
        }
    }
}

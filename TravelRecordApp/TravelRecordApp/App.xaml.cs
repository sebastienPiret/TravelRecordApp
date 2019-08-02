using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static MobileServiceClient MobileService  = new MobileServiceClient("https://travelrecordappseb.azurewebsites.net");

        public static IMobileServiceSyncTable<Post> postsTable;

        public static User user=new User();
        
        public App()
        {
            InitializeComponent();

#if DEBUG
            HotReloader.Current.Run(this, new HotReloader.Configuration
            {
                PreviewerDefaultMode = HotReloader.PreviewerMode.On
            });
#endif

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

#if DEBUG
            HotReloader.Current.Run(this, new HotReloader.Configuration
            {
                PreviewerDefaultMode = HotReloader.PreviewerMode.On
            });
#endif

            MainPage = new NavigationPage(new MainPage());
            DatabaseLocation = databaseLocation;

            var storage = new MobileServiceSQLiteStore(DatabaseLocation);
            storage.DefineTable<Post>();

            MobileService.SyncContext.InitializeAsync(storage);

            postsTable = MobileService.GetSyncTable<Post>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        private HomeVM viewModel;
        public HomePage()
        {
            InitializeComponent();

            viewModel=new HomeVM();
            BindingContext = viewModel;
            /*
            Children.Add(new ContentPage { Title = "Todo", IconImageSource = "add.png" });
            Children.Add(new ContentPage { Title = "Reminders", IconImageSource = "home.png" });
            Children.Add(new ContentPage { Title = "Contacts", IconImageSource = "map.png", });
            */
        }
    }
}
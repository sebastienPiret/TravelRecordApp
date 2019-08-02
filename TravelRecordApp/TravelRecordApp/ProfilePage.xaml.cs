using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        /**
         * On va chercher dans la sqlite la liste des différentes "Venue"
         * Que l'on ajoute dans un dictionnaire, accompagné du nombre de "Venue" semblables en utilisant LINQ
         */
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
                //var postTable = conn.Table<Post>().ToList();

                var postTable = await Post.Read();

                var categoriesCount = Post.PostCategories(postTable);

                categoriesListView.ItemsSource = categoriesCount;

                postCountLabel.Text = postTable.Count.ToString();
            //}
        }
    }
}
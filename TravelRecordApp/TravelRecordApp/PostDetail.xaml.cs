using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostDetail : ContentPage
    {
        private Post selectedPost;
        public PostDetail(Post selectedPost)
        {
            InitializeComponent();
            this.selectedPost = selectedPost;
            experienceEntry.Text = selectedPost.Experience;
        }

        private async void UpdateButton_OnClicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;

            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Update(selectedPost);

            //    if (rows > 0)
            //        DisplayAlert("Success", "Experience successfully updated", "ok");
            //    else
            //        DisplayAlert("Failure", "Update not successful", "bad");
            //}

            Post.Update(selectedPost);
            await AzureAppServiceHelper.SyncAsync();
            await DisplayAlert("Success", "Update is successful", "ok");
        }

        private async void DeleteButton_OnClicked(object sender, EventArgs e)
        {
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Delete(selectedPost);

            //    if (rows > 0)
            //        DisplayAlert("Success", "Experience successfully deleted", "ok");
            //    else
            //        DisplayAlert("Failure", "Deletion not successful", "bad");
            //}

            Post.Delete(selectedPost);
            await AzureAppServiceHelper.SyncAsync();
            await DisplayAlert("Success", "Deletion is successful", "ok");
        }
    }
}
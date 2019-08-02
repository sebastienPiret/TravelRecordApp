using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TravelRecordApp.Annotations;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class NewTravelVM : INotifyPropertyChanged
    {
        public PostCommand PostCommand { get; set; }
        private Post _post;

        public Post Post
        {
            get { return _post; }
            set
            {
                _post = value;
                OnPropertyChanged("Post");
            }
        }

        private string _experience;

        public string Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                Post=new Post()
                {
                    Experience = this.Experience,
                    Venue = this._venue
                };
                OnPropertyChanged("Experience");
            }
        }

        private Venue _venue;

        public Venue Venue
        {
            get { return _venue; }
            set
            {
                _venue = value;
                Post = new Post()
                {
                    Experience = this.Experience,
                    Venue = this._venue
                };
                OnPropertyChanged("Venue");
            }
        }

        public NewTravelVM()
        {
            PostCommand=new PostCommand(this);
            Post=new Post();
            Venue=new Venue();
        }

        /**
         * Used to record some comment from users into local database
         */
        public async void PublishPost(Post post)
        {
            try
            {

                Post.Insert(post);
                await App.Current.MainPage.DisplayAlert("Success", "Insertion is successful", "ok");
            }
            catch (NullReferenceException nre)
            {
                await App.Current.MainPage.DisplayAlert("selection", "You've to select a venue: " + nre.Message, "ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("selection", "Something went wrong: " + ex.Message, "ok");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

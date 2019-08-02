using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using SQLite;
using TravelRecordApp.Annotations;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {
        // Added by interface INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private string _id;

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _experience;

        public string Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                OnPropertyChanged("Experience");
            }
        }

        private string _venueName;

        public string VenueName
        {
            get { return _venueName; }
            set
            {
                _venueName = value;
                OnPropertyChanged("VenueName");
            }
        }

        private string _categoryId;

        public string CategoryId
        {
            get { return _categoryId; }
            set
            {
                _categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        private int _distance;

        public int Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private double _latitude;

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        private double _longitude;

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        private Venue _venue;
        [JsonIgnore]
        public Venue Venue
        {
            get { return _venue; }
            set
            {
                _venue = value;

                if (_venue.categories != null)
                {
                    var firstCategory = _venue.categories.FirstOrDefault();

                    if (firstCategory != null)
                    {
                        CategoryId = firstCategory.id;
                        CategoryName = firstCategory.name;
                    }
                }

                if (_venue.location != null)
                {
                    Address = _venue.location.address;
                    Longitude = _venue.location.lng;
                    Latitude = _venue.location.lat;
                    Distance = _venue.location.distance;
                }

                VenueName = _venue.name;
                UserId = App.user.Id;

                OnPropertyChanged("Venue");
            }
        }

        private DateTimeOffset createdat;

        public DateTimeOffset CREATEDAT
        {
            get { return createdat; }
            set
            {
                createdat = value;
                OnPropertyChanged("createdat");
            }
        }

        public static async void Insert(Post post)
        {
            await App.postsTable.InsertAsync(post);
            await App.MobileService.SyncContext.PushAsync();
        }

        public static async Task<List<Post>> Read()
        {
            var posts = await App.postsTable.Where(p => p.UserId == App.user.Id).ToListAsync();

            return posts;
        }

        public static async void Update(Post post)
        {
            await App.postsTable.UpdateAsync(post);
            await App.MobileService.SyncContext.PushAsync();
        }

        public static async void Delete(Post post)
        {
            await App.postsTable.DeleteAsync(post);
            await App.MobileService.SyncContext.PushAsync();
        }

        public static Dictionary<string, int> PostCategories(List<Post> posts)
        {
            var categories = (from p in posts orderby p.CategoryId select p.CategoryName).Distinct().ToList();
            // different Linq method for same result
            var categories2 = posts.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
            foreach (var category in categories2)
            {
                var count = (from post in posts where post.CategoryName == category select post).ToList().Count;

                // different Linq method for same result
                var count2 = posts.Where(p => p.CategoryName == category).ToList().Count;

                categoriesCount.Add(category, count2);
            }

            return categoriesCount;
        }

        

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

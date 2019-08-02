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
    public class MainVM : INotifyPropertyChanged
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }
        public LoginCommand LoginCommand { get; set; }
        public RegisterNavigationCommand registerNavigationCommand { get; set; }

        private string _email;

        public string email
        {
            get { return _email; }
            set
            {
                _email = value;
                User = new User()
                {
                    Email=this.email,
                    Password = this.password
                };
                OnPropertyChanged("email");
            }
        }

        private string _password;

        public string password
        {
            get { return _password; }
            set
            {
                _password = value;
                User = new User()
                {
                    Email = this.email,
                    Password = this.password
                };
                OnPropertyChanged("password");
            }
        }

        public MainVM()
        {
            User = new User();
            LoginCommand=new LoginCommand(this);
            registerNavigationCommand=new RegisterNavigationCommand(this);
        }

        public async void Login()
        {

            bool logged = await User.Login(User.Email, User.Password);
            if (logged)
            {
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Attention", "Please try again", "Ok");
            }
        }

        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

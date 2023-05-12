using FriendsAndroid.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FriendsAndroid
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new FriendListPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

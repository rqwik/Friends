using Plugin.Messaging;
using FriendsAndroid.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FriendsAndroid.VeiwModels
{
    public class FriendsListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FriendViewModel> Friends { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreateFriendCommand { get; set; }
        public ICommand DeleteFriendCommand { get; set; }
        public ICommand SaveFriendCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand SmsCommand { get; set; }
        public ICommand CallCommand { get; set; }
        public ICommand MailCommand { get; set; }


        FriendViewModel selectedFriend;
        public INavigation Navigation { get; set; }

        public FriendsListViewModel()
        {
            Friends = new ObservableCollection<FriendViewModel>();
            CreateFriendCommand = new Command(CreateFriend);
            DeleteFriendCommand = new Command(DeleteFriend);
            SaveFriendCommand = new Command(SaveFriend);
            BackCommand = new Command(Back);
            SmsCommand = new Command(Sms);
            CallCommand = new Command(PhoneDialer);
            MailCommand = new Command(Mail);
        } 

        public FriendViewModel SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                if (selectedFriend != value)
                {
                    FriendViewModel tempFriend = value;
                    selectedFriend = null;
                    OnPropertyChanged("SelectedFriend");
                    Navigation.PushAsync(new FriendPage(tempFriend));
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void CreateFriend()
        {
            Navigation.PushAsync(new FriendPage(new FriendViewModel() { ListViewModel = this}));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void SaveFriend(object friendObject)
        {
            FriendViewModel friend = friendObject as FriendViewModel;
            if (friend != null && friend.IsValid && !Friends.Contains(friend))
            {
                Friends.Add(friend);
            }
            Back();
        }

        private void DeleteFriend(object friendObject)
        {
            FriendViewModel friend = friendObject as FriendViewModel;
            if (friend != null)
            {
                Friends.Remove(friend);
            }
            Back();
        }
        private void Sms(object friendObject)
        {
            FriendViewModel friend = friendObject as FriendViewModel;
            var sms = CrossMessaging.Current.SmsMessenger;
            if (sms.CanSendSms)
                sms.SendSms(friend.Phone);
        }
        private void PhoneDialer(object friendObject)
        {
            FriendViewModel friend = friendObject as FriendViewModel;
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(friend.Phone);
        }
        private void Mail(object friendObject)
        {
            FriendViewModel friend = friendObject as FriendViewModel;
            var mail = CrossMessaging.Current.EmailMessenger;
            if (mail.CanSendEmail)
                mail.SendEmail(friend.Email, "", "текст письма");
        }
    }
}

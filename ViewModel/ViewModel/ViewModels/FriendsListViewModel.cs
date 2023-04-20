using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using ViewModel.Views;
using Xamarin.Forms;

namespace ViewModel.ViewModels
{
    public class FriendsListViewModel
    {
        public ObservableCollection<FriendViewModel> Friends { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreateFriendCommand { get; set; }
        public ICommand DeleteFriendCommand { get; set; }
        public ICommand SaveFriendCommand { get; set; }
        public ICommand BackCommand { get; set; }
        FriendViewModel selectedFriend;
        public INavigation Navigation { get; set; }


        public FriendsListViewModel() 
        {
            Friends = new ObservableCollection<FriendViewModel>();
            CreateFriendCommand = new Command(CreateFriend);
            DeleteFriendCommand = new Command(DeleteFriend);
            SaveFriendCommand = new Command(SaveFriend);
            BackCommand = new Command(Back);
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void SaveFriend(object friendObject)
        {
            FriendViewModel friend = friendObject as FriendViewModel;
            if (friend != null && friend.Isvalid && !Friends.Contains(friend))
            {
                Friends.Add(friend);
            }
            Back();
        }

        private void CreateFriend()
        {
            Navigation.PopAsync(new FriendPage( new FriendViewModel() { ListViewModel = this }));
        }

        public FriendViewModel SelectedFriend
        {
            get { return selectedFriend; }
            set
            { 
                if(selectedFriend != value)
                {
                    FriendViewModel tempFriend = value;
                    selectedFriend = value;
                    OnPropertyChanged("SelectedFriend");
                    Navigation.PushAsync(new FriendPage(tempFriend));
                }
            }
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
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

    }
}


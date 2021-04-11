using GPSNotepad.Model.Tables;
using GPSNotepad.Model.Tables.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GPSNotepad.Model.Extentions.RealmExtentions
{
    public static class RealmExtentions
    {
        public static PlaceViewModel ToViewModel(this Place realmPlace)
        {
            PlaceViewModel basePlace = new PlaceViewModel
            {
                Address = realmPlace.Address,
                Icon = realmPlace.Icon,
                PlaceId = realmPlace.PlaceId,
                PlaceName = realmPlace.PlaceName,
                Favorite = realmPlace.Favorite,
                UserId = realmPlace.UserId,
                Position = new Xamarin.Forms.Maps.Position
                (
                    realmPlace.Position.Latitude,
                    realmPlace.Position.Longitude
                )
            };
            return basePlace;
        }

        public static Place ToRealmPlace(this PlaceViewModel PlaceViewModel)
        {
            Place realmPlace = new Place
            {
                Address = PlaceViewModel.Address,
                Icon = PlaceViewModel.Icon,
                PlaceId = PlaceViewModel.PlaceId,
                PlaceName = PlaceViewModel.PlaceName,
                Favorite = PlaceViewModel.Favorite,
                UserId = PlaceViewModel.UserId,
                Position = new Position
                (
                    PlaceViewModel.Position.Latitude,
                    PlaceViewModel.Position.Longitude
                ),
            };
            return realmPlace;
        }

        public static User ToRealmUser(this UserViewModel UserViewModel)
        {
            User realmUser = new User
            {
                Mail = UserViewModel.Mail,
                Name = UserViewModel.Name,
                Password = UserViewModel.Password,
                UserId = UserViewModel.UserId
            };
            return realmUser;
        }

        public static UserViewModel ToViewModel(this User User)
        {
            UserViewModel UserViewModel = new UserViewModel
            {
                Mail = User.Mail,
                Name = User.Name,
                Password = User.Password,
                UserId = User.UserId
            };
            return UserViewModel;
        }

        public static ObservableCollection<UserViewModel> ToViewModel (this IEnumerable<User> realmUser)
        {
            var UserViewModelCollection = new ObservableCollection<UserViewModel>();

            foreach (var item in realmUser)
            {
                UserViewModelCollection.Add(item.ToViewModel());
            }

            return UserViewModelCollection;
        }

        public static ObservableCollection<PlaceViewModel> ToViewModel(this IEnumerable<Place> realmPlace)
        {
            var BasePlace = new ObservableCollection<PlaceViewModel>();

            foreach (Place item in realmPlace)
            {
                BasePlace.Add(item.ToViewModel());
            }

            return BasePlace;
        }
    }
}

using GPSNotepad.Model.Extentions.RealmExtentions;
using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using GPSNotepad.Servises.PlaceService;
using GPSNotepad.Servises.UserService;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GPSNotepad.Services.PlaceService
{
    public class PlaceService: IPlaceService
    {
        private readonly IRepository repository;
        private readonly IUserService userService;

        public PlaceService(IRepository repository, IUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        public void AddPlace(PlaceViewModel placeViewModel)
        {
            placeViewModel.UserId = userService.GetCurrentUser().UserId;
            repository.AddItem(placeViewModel.ToRealmPlace());
        }

        public void EditPlace(PlaceViewModel placeViewModel)
        {
            placeViewModel.UserId = userService.GetCurrentUser().UserId;
            repository.EditItem(placeViewModel.ToRealmPlace());
        }

        public ObservableCollection<PlaceViewModel> GetUserPlaces()
        {
            return new ObservableCollection<Place>(repository.GetAll<Place>().Result).Where(u => userService.GetCurrentUser().ToRealmUser().UserId == u.UserId).ToViewModel();
        }

        public void RemovePlace(PlaceViewModel placeViewModel)
        {
            repository.RemoveItem(placeViewModel.ToRealmPlace());
        }
    }
}

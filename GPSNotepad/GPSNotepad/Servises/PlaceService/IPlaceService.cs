using GPSNotepad.Model.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GPSNotepad.Servises.PlaceService
{
    public interface IPlaceService
    {
        ObservableCollection<PlaceViewModel> GetUserPlaces();
        void AddPlace(PlaceViewModel placeViewModel);
        void EditPlace(PlaceViewModel placeViewModel);
        void RemovePlace(PlaceViewModel placeViewModel);
    }
}

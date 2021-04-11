using Prism.Mvvm;
using System;

namespace GPSNotepad.Model.Tables
{
    public class PlaceViewModel: BindableBase
    {
        private string _placeId = Guid.NewGuid().ToString();
        public string PlaceId
        {
            get => _placeId;
            set => SetProperty(ref _placeId, value);
        }
        private string _userId = "";
        public string UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }
        private string _placeName = "";
        public string PlaceName
        {
            get => _placeName;
            set => SetProperty(ref _placeName, value);
        }
        private string _address = "";
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        private string _icon = "";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }
        private bool _favorite;
        public bool Favorite
        {
            get => _favorite;
            set => SetProperty(ref _favorite, value);
        }
        private Xamarin.Forms.Maps.Position _position;
        public Xamarin.Forms.Maps.Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }
        private int _clusteringCount = 1;
        public int ClusteringCount
        {
            get => _clusteringCount;
            set => SetProperty(ref _clusteringCount, value);
        }
    }
}

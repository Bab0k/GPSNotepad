using Realms;
using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace GPSNotepad.Model.Tables
{
    public class RealmPlace: RealmObject, IEnumerable
    {
        [PrimaryKey]
        public string PlaceId { get; set; } = Guid.NewGuid().ToString();
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public bool Favorite { get; set; } = false;
        public Position Position { get; set; }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Position : RealmObject
    {
        public Position() { }
        public Position(double Latitude, double Longitude)
        {
            this.Longitude = Longitude;
            this.Latitude = Latitude;
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    static class RealmPlaceExtentions
    {
        public static BasePlace ToBasePlace(this RealmPlace realmPlace)
        {
            BasePlace basePlace = new BasePlace
            {
                Address = realmPlace.Address,
                Icon = realmPlace.Icon,
                PlaceId = realmPlace.PlaceId,
                Position = new Xamarin.Forms.Maps.Position
                (
                    realmPlace.Position.Latitude,
                    realmPlace.Position.Longitude
                ),
                PlaceName = realmPlace.PlaceName
            };

            return basePlace;
        }
        public static RealmPlace ToRealmPlace(this BasePlace basePlace)
        {
            RealmPlace realmPlace = new RealmPlace
            {
                Address = basePlace.Address,
                Icon = basePlace.Icon,
                PlaceId = basePlace.PlaceId,
                PlaceName = basePlace.PlaceName,
                Position = new Position
                (
                    basePlace.Position.Latitude,
                    basePlace.Position.Longitude
                ),
            };
            return realmPlace;
        }

        public static ObservableCollection<BasePlace> ToBasePlace(this ObservableCollection<RealmPlace> realmPlace)
        {
            var BasePlace = new ObservableCollection<BasePlace>();

            foreach (RealmPlace item in realmPlace)
            {
                BasePlace.Add(item.ToBasePlace());
            }

            return BasePlace;
        }
        public static ObservableCollection<RealmPlace> ToRealmPlace(this ObservableCollection<BasePlace> BasePlace)
        {
            var RealmPlace = new ObservableCollection<RealmPlace>();

            foreach (BasePlace item in BasePlace)
            {
                RealmPlace.Add(item.ToRealmPlace());
            }

            return RealmPlace;
        }

    }

}

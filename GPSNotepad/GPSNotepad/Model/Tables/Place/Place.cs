using Realms;
using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace GPSNotepad.Model.Tables
{
    public class Place: RealmObject, IEnumerable
    {
        [PrimaryKey]
        public string PlaceId { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = "";
        public string PlaceName { get; set; } = "";
        public string Address { get; set; } = "";
        public string Icon { get; set; } = "";
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
}

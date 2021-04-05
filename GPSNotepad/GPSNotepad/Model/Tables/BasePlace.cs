using System;

namespace GPSNotepad.Model.Tables
{
    public class BasePlace
    {
        public string PlaceId { get; set; } = Guid.NewGuid().ToString();
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public bool Favorite { get; set; } = false;
        public Xamarin.Forms.Maps.Position Position { get; set; }
    }
}

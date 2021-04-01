using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad.Model.Tables
{
    public class BasePlace
    {
        public string PlaceId { get; set; } = Guid.NewGuid().ToString();
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public Xamarin.Forms.Maps.Position Position { get; set; }
    }
}

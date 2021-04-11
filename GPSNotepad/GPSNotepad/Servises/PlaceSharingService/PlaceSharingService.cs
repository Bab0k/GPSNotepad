using GPSNotepad.Controls;
using GPSNotepad.Model.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace GPSNotepad.Servises.PlaceSharingService
{
    public class PlaceSharingService : IPlaceSharingService
    {
        public PlaceViewModel ParsingSharingPin(Uri uri)
        {
            PlaceViewModel myCustomPin = new PlaceViewModel
            {
                PlaceName = uri.Segments[2].Replace("/", ""),
                Address = uri.Segments[3].Replace("/", "")
            };
            if (double.TryParse(uri.Segments[4].Replace("/", ""), out var Latitude) &&
                double.TryParse(uri.Segments[5].Replace("/", ""), out var Longitude))
            {
                myCustomPin.Position = new Xamarin.Forms.Maps.Position(Latitude, Longitude);
            }
            myCustomPin.Favorite = true;

            return myCustomPin;
        }
        public string PinConvertToString(PlaceViewModel customPin)
        {
            return $"{Resources.Host}/{Resources.Action}/{customPin.PlaceName}/{customPin.Address}/{customPin.Position.Latitude}/{customPin.Position.Longitude}";
        }

        public async void SendPinAsync(PlaceViewModel customPin)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = PinConvertToString(customPin)
            });
        }
    }
}

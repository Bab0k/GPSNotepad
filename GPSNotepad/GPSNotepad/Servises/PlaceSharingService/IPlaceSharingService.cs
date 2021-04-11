using GPSNotepad.Controls;
using GPSNotepad.Model.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad.Servises.PlaceSharingService
{
    public interface IPlaceSharingService
    {
        void SendPinAsync(PlaceViewModel customPin);
        PlaceViewModel ParsingSharingPin(Uri url);
        string PinConvertToString(PlaceViewModel customPin);
    }
}

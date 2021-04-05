using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad.Model.Interface
{
    public interface IViewActionsHandler
    {
        void OnAppearing();
        void OnDisappearing();
    }
}

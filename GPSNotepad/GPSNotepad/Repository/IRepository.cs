using GPSNotepad.Model.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GPSNotepad.Repository
{
    public interface IRepository
    {
        ObservableCollection<BasePlace> GetPlaces();
        void AddPlace(BasePlace place);
        void RemovePlace(string id);
        void EditPlace(BasePlace place); 

    }
}

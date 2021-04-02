using GPSNotepad.Model.Tables;
using System.Collections.ObjectModel;

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

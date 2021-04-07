using GPSNotepad.Model.Tables;
using Realms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GPSNotepad.Repository
{
    public interface IRepository
    {
        Task<IQueryable<T>> GetAll<T>() where T: RealmObject;
        Task AddItem<T>(T Item) where T: RealmObject;
        Task EditItem<T>(T Item) where T: RealmObject;
        Task RemoveItem<T>(T Item) where T: RealmObject;

    }
}

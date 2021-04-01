using GPSNotepad.Model.Tables;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GPSNotepad.Repository
{
    class RealmRepository : IRepository
    {
        public void AddPlace(BasePlace place)
        {
            Realm _realm = Realm.GetInstance();
            Transaction _transaction = _realm.BeginWrite();

            _realm.Add(place.ToRealmPlace());

            _transaction.Commit();
            _transaction.Dispose();
        }

        public void EditPlace(BasePlace place)
        {
            Realm _realm = Realm.GetInstance();
            Transaction _transaction = _realm.BeginWrite();

            _realm.Add(place.ToRealmPlace(), true);

            _transaction.Commit();
            _transaction.Dispose();
        }

        public ObservableCollection<BasePlace> GetPlaces()
        {
            Realm _realm = Realm.GetInstance();

            return new ObservableCollection<RealmPlace>(_realm.All<RealmPlace>()).ToBasePlace();
        }

        public void RemovePlace(string id)
        {
            Realm realm = Realm.GetInstance();
            var Item = realm.All<RealmPlace>().First(u => u.PlaceId == id);

            using (var trans = realm.BeginWrite())
            {
                realm.Remove(Item);
                trans.Commit();
            }
        }
    }
}

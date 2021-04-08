using GPSNotepad.Model.Extentions.RealmExtentions;
using GPSNotepad.Model.Tables;
using GPSNotepad.Model.Tables.User;
using GPSNotepad.Repository;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSNotepad.Servises.Repository
{
    public class Repository : IRepository
    {
        static readonly Realm _realm;
        static Repository()
        {
            _realm = Realm.GetInstance();
        }

        public Task<IQueryable<T>> GetAll<T>() where T : RealmObject
        {
            return Task.FromResult(_realm.All<T>());
        }

        public Task AddItem<T>(T Item) where T : RealmObject
        {
            Transaction _transaction = _realm.BeginWrite();

            _realm.Add(Item);

            _transaction.Commit();
            _transaction.Dispose();

            return Task.CompletedTask;
        }
        public Task EditItem<T>(T Item) where T : RealmObject
        {
            Transaction _transaction = _realm.BeginWrite();

            _realm.Add(Item, true);

            _transaction.Commit();
            _transaction.Dispose();
            return Task.CompletedTask;
        }

        public Task RemoveItem<T>(T Item) where T : RealmObject
        {
            Transaction _transaction = _realm.BeginWrite();

            _realm.Remove(Item);

            _transaction.Commit();
            _transaction.Dispose();
            return Task.CompletedTask;
        }

    }
}

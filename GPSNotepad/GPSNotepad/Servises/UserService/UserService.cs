using GPSNotepad.Model.Extentions.RealmExtentions;
using GPSNotepad.Model.Tables;
using GPSNotepad.Model.Tables.User;
using GPSNotepad.Repository;
using GPSNotepad.Servises.UserService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GPSNotepad.Model.Servises
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private UserViewModel CurrentUser;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }
        public void AddUser(UserViewModel userViewModel)
        {
            if (!IsContains(userViewModel))
            {
                repository.AddItem(userViewModel.ToRealmUser());

                SetCurrentUser(userViewModel);
            }
            else
            {
                // ToDo alert 
            }
        }

        public ObservableCollection<UserViewModel> GetAllUsers()
        {
            return new ObservableCollection<User>(repository.GetAll<User>().Result).ToViewModel();
        }
        public void SetCurrentUser(UserViewModel userViewModel)
        {
            CurrentUser = userViewModel;
        }
        public UserViewModel GetCurrentUser()
        {
            return CurrentUser;
        }

        public bool IsContains(UserViewModel userViewModel)
        {
            return GetAllUsers().Where(u => userViewModel.Mail == u.Mail).Any();
        }
    }
}

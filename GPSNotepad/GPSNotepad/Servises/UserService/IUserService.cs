using GPSNotepad.Model.Tables.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GPSNotepad.Servises.UserService
{
    public interface IUserService
    {
        UserViewModel GetCurrentUser();
        void SetCurrentUser(UserViewModel userViewModel);

        void AddUser(UserViewModel userViewModel);
        ObservableCollection<UserViewModel> GetAllUsers();
        bool IsContains(UserViewModel userViewModel);
    }
}

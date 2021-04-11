using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad.Model.Tables.User
{
    public class UserViewModel: BindableBase
    {
        private string _userId = Guid.NewGuid().ToString();
        public string UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }
        private string _Name = "";
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }
        private string _mail = Guid.NewGuid().ToString();
        public string Mail
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }
        private string _password = "";
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
    }
}

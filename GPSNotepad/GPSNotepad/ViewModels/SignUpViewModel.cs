using GPSNotepad.Model.Tables.User;
using GPSNotepad.Servises.UserService;
using GPSNotepad.Views;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace GPSNotepad.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        string mail = string.Empty;
        public string Mail
        {
            get => mail;
            set => SetProperty(ref mail, value);

        }

        string password = string.Empty;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        string name = string.Empty;
        private readonly IUserService userService;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);

        }

        public SignUpViewModel(INavigationService navigationService, IUserService userService) : base(navigationService)
        {
            this.userService = userService;
        }
        public DelegateCommand OnSignUpCommand => new DelegateCommand(SignUpCommand);

        private void SignUpCommand()
        {
            if (Validation.Validation.IsMail(Mail) && Validation.Validation.IsPassword(password) && Validation.Validation.IsName(Name))
            {
                userService.AddUser(new UserViewModel
                {
                    Mail = Mail,
                    Password = Password,
                    Name = Name
                });

                NavigationService.NavigateAsync($"/{nameof(MainPageView)}");
            }
            else
            {
                //ToDo: Alert
            }
        }
}
}

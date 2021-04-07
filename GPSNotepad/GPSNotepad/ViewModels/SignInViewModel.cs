using GPSNotepad.Servises.AuthorizeService;
using GPSNotepad.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GPSNotepad.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        string mail = string.Empty;
        public string Mail
        {
            get => mail;
            set => SetProperty(ref mail, value);

        }

        string password = string.Empty;
        private readonly IAuthorizeService authorizeService;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public SignInViewModel(INavigationService navigationService, IAuthorizeService authorizeService) : base(navigationService)
        {
            this.authorizeService = authorizeService;
        }

        public DelegateCommand OnSignInCommand => new DelegateCommand(SignInCommand);

        private async void SignInCommand()
        {
            if (authorizeService.Authorize(Mail, Password))
            {
                await NavigationService.NavigateAsync($"/{nameof(MainPageView)}");
            }
            else
            {
                //ToDo: Alert
            }
        }
        public DelegateCommand OnSignUpViewNavigatioCommand => new DelegateCommand(SignUpViewNavigatioCommand);

        private async void SignUpViewNavigatioCommand()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }
    }
}

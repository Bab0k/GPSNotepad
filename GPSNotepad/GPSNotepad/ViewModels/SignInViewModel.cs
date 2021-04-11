using GPSNotepad.Servises.AuthorizeService;
using GPSNotepad.Servises.PlaceSharingService;
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
        #region -- Private Variables/Properties -- 

        private string password = string.Empty;
        private readonly IAuthorizeService authorizeService;
        private readonly IPlaceSharingService placeSharingService;

        #endregion

        #region -- Constructors --

        public SignInViewModel(INavigationService navigationService, IAuthorizeService authorizeService, IPlaceSharingService placeSharingService) : base(navigationService)
        {
            this.authorizeService = authorizeService;
            this.placeSharingService = placeSharingService;
        }

        #endregion

        #region -- Public properties --

        private string mail = string.Empty;
        public string Mail
        {
            get => mail;
            set => SetProperty(ref mail, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public DelegateCommand OnSignInCommand => new DelegateCommand(SignInCommand);
        public DelegateCommand OnSignUpViewNavigatioCommand => new DelegateCommand(SignUpViewNavigatioCommand);

        #endregion

        #region -- Private helpers -- 

        private async void SignInCommand()
        {
            if (authorizeService.Authorize(Mail, Password))
            {
                await NavigationService.NavigateAsync($"/{nameof(MainPageView)}");
            }
            else
            {
            }
        }

        private async void SignUpViewNavigatioCommand()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }

        #endregion
    }
}

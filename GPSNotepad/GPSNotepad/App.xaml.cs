using GPSNotepad.Model.Servises;
using GPSNotepad.Repository;
using GPSNotepad.Services.PlaceService;
using GPSNotepad.Servises.AuthorizeService;
using GPSNotepad.Servises.PlaceService;
using GPSNotepad.Servises.PlaceSharingService;
using GPSNotepad.Servises.UserService;
using GPSNotepad.ViewModels;
using GPSNotepad.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using System;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace GPSNotepad
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }
        IAuthorizeService AuthorizeService => Container.Resolve<AuthorizeService>();
        protected override async void OnInitialized()
        {
            InitializeComponent();

            Realms.RealmConfiguration realmConfiguration = new Realms.RealmConfiguration
            {
                SchemaVersion = 4
            };
            Realms.RealmConfiguration.DefaultConfiguration = realmConfiguration;

            if (AuthorizeService.IsAuthorize())
            {
                await NavigationService.NavigateAsync($"/{nameof(MainPageView)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
        }
        protected override async void OnAppLinkRequestReceived(Uri uri)
        {
            if (uri.Host.EndsWith("GPSNotePad.com", StringComparison.OrdinalIgnoreCase))
            {
                if (uri.Segments != null && uri.Segments.Length == 6)
                {
                    var action = uri.Segments[1].Replace("/", "");
                    if (action == "Location")
                    {
                        await NavigationService.NavigateAsync($"/{nameof(MapView)}", new NavigationParameters
                        {
                            { "Uri", uri }
                        });
                    }
                }
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPageView, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MapView, MapViewModel>();
            containerRegistry.RegisterForNavigation<PinListView, PinListViewModel>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPinView, AddEditPinViewModel>();

            containerRegistry.RegisterSingleton<IRepository, Servises.Repository.Repository>();
            containerRegistry.RegisterSingleton<IPlaceService, PlaceService>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IAuthorizeService, AuthorizeService>();
            containerRegistry.RegisterSingleton<IPlaceSharingService, PlaceSharingService>();
        }
    }
}

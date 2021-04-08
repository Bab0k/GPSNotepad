using GPSNotepad.Model.Servises;
using GPSNotepad.Repository;
using GPSNotepad.Services.PlaceService;
using GPSNotepad.Servises.AuthorizeService;
using GPSNotepad.Servises.PlaceService;
using GPSNotepad.Servises.UserService;
using GPSNotepad.ViewModels;
using GPSNotepad.Views;
using Prism;
using Prism.Ioc;
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

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Realms.RealmConfiguration realmConfiguration = new Realms.RealmConfiguration
            {
                SchemaVersion = 4
            };
            Realms.RealmConfiguration.DefaultConfiguration = realmConfiguration;

            IAuthorizeService authorizeService = Container.Resolve<AuthorizeService>();

            if (authorizeService.IsAuthorize())
            {
                await NavigationService.NavigateAsync($"/{nameof(MainPageView)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
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
        }
    }
}

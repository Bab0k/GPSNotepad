using GPSNotepad.Repository;
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

            Realms.RealmConfiguration realmConfiguration = new Realms.RealmConfiguration();
            Realms.RealmConfiguration.DefaultConfiguration = realmConfiguration;

            Realms.Realm realm = Realms.Realm.GetInstance();

            Realms.Transaction transaction = realm.BeginWrite();

            realm.RemoveAll();

            transaction.Commit();
            transaction.Dispose();



            await NavigationService.NavigateAsync($"/{nameof(MainPageView)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPageView, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MapView, MapViewModel>();
            containerRegistry.RegisterForNavigation<PinListView, PinListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPinView, AddEditPinViewModel>();

            containerRegistry.RegisterSingleton<IRepository, RealmRepository>();
        }
    }
}

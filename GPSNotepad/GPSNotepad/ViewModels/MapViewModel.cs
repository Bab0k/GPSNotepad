using GPSNotepad.Controls;
using GPSNotepad.Model.Interface;
using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using GPSNotepad.Servises.PlaceService;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GPSNotepad.ViewModels
{
    public class MapViewModel : ViewModelBase, IViewActionsHandler
    {
        IPlaceService PlaceService;

        ObservableCollection<PlaceViewModel> pins = new ObservableCollection<PlaceViewModel>();
        public ObservableCollection<PlaceViewModel> Pins 
        {
            get => pins; 
            set => SetProperty(ref pins, value); 
        }
        MapSpan mapSpan;
        public MapSpan MapSpan
        {
            get => mapSpan;
            set => SetProperty(ref mapSpan, value);
        }

        bool pinClicked;
        public bool PinClicked
        {
            get => pinClicked;
            set => SetProperty(ref pinClicked, value);
        }

        string markDescription;
        public string MarkDescription
        {
            get => markDescription;
            set => SetProperty(ref markDescription, value);
        }
        string markName;
        public string MarkName
        {
            get => markName;
            set => SetProperty(ref markName, value);
        }
        string markLongitude;
        public string MarkLongitude
        {
            get => markLongitude;
            set => SetProperty(ref markLongitude, value);
        }
        string markLatitude;
        public string MarkLatitude
        {
            get => markLatitude;
            set => SetProperty(ref markLatitude, value);
        }



        public MapViewModel(INavigationService navigationService, IPlaceService PlaceService) : base(navigationService)
        {
            this.PlaceService = PlaceService;
        }

        public DelegateCommand OnUpdateMap => new DelegateCommand(OnUpdateMapCommand);

        private void OnUpdateMapCommand()
        {
            Pins = PlaceService.GetUserPlaces();
        }
        public ICommand OnMarkClickedCommand => new Command<MyCustomPin>(MarkClickedCommand);

        private void MarkClickedCommand(MyCustomPin obj)
        {
            PinClicked = true;
            MarkDescription = obj.Address;
            MarkName = obj.Label;
            MarkLatitude = obj.Position.Latitude.ToString();
            MarkLongitude = obj.Position.Longitude.ToString();
        }
        public ICommand OnMapClickedCommand => new Command<object>(MapClickedCommand);

        private void MapClickedCommand(object obj)
        {
            PinClicked = false;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            PlaceViewModel SelectedPlace;
            if (parameters.TryGetValue(nameof(SelectedPlace), out SelectedPlace))
            {
                Random rand = new Random();
                MapSpan = new MapSpan(SelectedPlace.Position, 1 + rand.NextDouble()/100, 1 + rand.NextDouble()/100);
            }
        }

        public override void OnAppearing()
        {
            OnUpdateMap?.Execute();
        }
    }
}

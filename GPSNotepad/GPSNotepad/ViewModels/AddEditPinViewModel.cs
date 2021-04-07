using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using GPSNotepad.Servises.PlaceService;
using Prism.Navigation;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GPSNotepad.ViewModels
{
    public class AddEditPinViewModel : ViewModelBase, INavigationAware
    {
        IPlaceService PlaceService;

        private PlaceViewModel _place;
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        private MapSpan mapSpan;
        public MapSpan MapSpan
        {
            get => mapSpan;
            set => SetProperty(ref mapSpan, value);
        }


        private double lantitude;
        public string Latitude
        {
            get => lantitude.ToString();
            set
            {
                if (double.TryParse(value, out var lant))
                {
                    SetProperty(ref lantitude, lant);
                    Position = new Xamarin.Forms.Maps.Position(lantitude, position.Longitude);
                }
            }
        }

        double longitude;
        public string Longitude
        {
            get => longitude.ToString();
            set
            {
                if (double.TryParse(value, out var lng))
                {
                    SetProperty(ref longitude, lng);
                    Position = new Xamarin.Forms.Maps.Position(Position.Latitude, longitude);
                }
            }
        }
        Xamarin.Forms.Maps.Position position = new Xamarin.Forms.Maps.Position();
        public Xamarin.Forms.Maps.Position Position
        {
            get => position;
            set 
            {
                if (position == value)
                {
                    return;
                }
                SetProperty(ref position, value);
            }
        }

        public AddEditPinViewModel(INavigationService navigationService, IPlaceService PlaceService) : base(navigationService)
        {
            this.PlaceService = PlaceService;
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Latitude))
            {
                double lant;

                if (double.TryParse(Latitude, out lant))
                {
                    if (lant > 90) 
                    {
                        Latitude = "90";
                    }
                    if (lant < -90)
                    {
                        Latitude = "-90";
                    }
                }
            }
            if (args.PropertyName == nameof(Latitude))
            {
                double lng;

                if (double.TryParse(Longitude, out lng))
                {
                    if (lng > 180)
                    {
                        Longitude = "180";
                    }
                    if (lng < -180)
                    {
                        Longitude = "-180";
                    }
                }
            }

            if (args.PropertyName == nameof(Position) || args.PropertyName == nameof(Description))
            {
                Pin pin = new Pin()
                {
                    Position = Position,
                    Label = Description,

                };

                MapSpan = new MapSpan(Position, 1, 1);
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            _place.PlaceName = Name;
            _place.Address = Description;
            _place.Position = Position;

            PlaceService.EditPlace(_place);

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(nameof(_place), out _place))
            {
                Latitude = _place.Position.Latitude.ToString();
                Longitude = _place.Position.Longitude.ToString();
                Name = _place.PlaceName;
                Description = _place.Address;
            }
            else
            {
                _place = new PlaceViewModel();
            }
        }


        public ICommand OnMapClickCommand => new Command<MapClickedEventArgs>(OnMapClick);

        private void OnMapClick(MapClickedEventArgs e)
        {
            Longitude = e.Position.Longitude.ToString();
            Latitude = e.Position.Latitude.ToString();
        }

    }
}

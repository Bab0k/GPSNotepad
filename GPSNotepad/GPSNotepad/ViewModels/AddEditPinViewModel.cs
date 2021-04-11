using GPSNotepad.Controls;
using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using GPSNotepad.Servises.PlaceService;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GPSNotepad.ViewModels
{
    public class AddEditPinViewModel : ViewModelBase, INavigationAware
    {

        #region Private Variables/Properties

        private readonly IPlaceService PlaceService;

        private PlaceViewModel _place;

        #endregion

        #region Constructors
        public AddEditPinViewModel(INavigationService navigationService, IPlaceService PlaceService) : base(navigationService)
        {
            this.PlaceService = PlaceService;
        }

        #endregion

        #region -- Public properties --

        #region Bindble Properties

        private string name = "";
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string description = "";
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

        private double longitude;
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
        
        private ObservableCollection<MyCustomPin> pin = new ObservableCollection<MyCustomPin>();
        public ObservableCollection<MyCustomPin> SinglePin
        {
            get => pin;
            set => SetProperty(ref pin, value);
        }

        #endregion
       
        public ICommand OnMapClickCommand => new Command<MapClickedEventArgs>(OnMapClick);

        #endregion

        #region -- Iterface implementation --

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


        #endregion

        #region -- Protected implementation --
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Latitude))
            {
                if (double.TryParse(Latitude, out double lant))
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

                if (double.TryParse(Longitude, out double lng))
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
                MyCustomPin newPin = new MyCustomPin()
                {
                    Position = Position,
                    Label = Name,
                };

                SinglePin.Clear();

                SinglePin.Add(newPin);
                MapSpan = new MapSpan(Position, 1, 1);
            }
        }

        #endregion

        #region -- Private helpers -- 

        private void OnMapClick(MapClickedEventArgs e)
        {
            Longitude = e.Position.Longitude.ToString();
            Latitude = e.Position.Latitude.ToString();
        }
      
        #endregion
   
    }
}

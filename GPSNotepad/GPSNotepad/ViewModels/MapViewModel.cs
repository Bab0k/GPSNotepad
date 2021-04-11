using GPSNotepad.Controls;
using GPSNotepad.Model.Interface;
using GPSNotepad.Model.Servises;
using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using GPSNotepad.Servises.PlaceService;
using GPSNotepad.Servises.PlaceSharingService;
using GPSNotepad.Servises.UserService;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GPSNotepad.ViewModels
{
    public class MapViewModel : ViewModelBase, IViewActionsHandler
    {

        #region Private Variables/Properties

        private readonly IPlaceService PlaceService;
        private readonly IPlaceSharingService placeSharingService;
        private readonly IUserService userService;

        #endregion

        #region Constructors
        public MapViewModel(INavigationService navigationService, IPlaceService PlaceService, IPlaceSharingService placeSharingService, IUserService userService) : base(navigationService)
        {
            this.PlaceService = PlaceService;
            this.placeSharingService = placeSharingService;
            this.userService = userService;
        }

        #endregion

        #region -- Public properties --

        #region Bindble Properties

        ObservableCollection<PlaceViewModel> pins = new ObservableCollection<PlaceViewModel>();
        public ObservableCollection<PlaceViewModel> Pins
        {
            get => pins;
            set => SetProperty(ref pins, value);
        }
        ObservableCollection<PlaceViewModel> clusteringPins = new ObservableCollection<PlaceViewModel>();
        public ObservableCollection<PlaceViewModel> ClusteringPins
        {
            get => clusteringPins;
            set => SetProperty(ref clusteringPins, value);
        }
        MapSpan mapSpan;
        public MapSpan MapSpan
        {
            get => mapSpan;
            set => SetProperty(ref mapSpan, value);
        }
        MapSpan oldMapCamera = new MapSpan(new Xamarin.Forms.Maps.Position(0, 0), 0, 0);
        public MapSpan OldMapCamera
        {
            get => oldMapCamera;
            set => SetProperty(ref oldMapCamera, value);
        }
        bool pinClicked;
        public bool PinClicked
        {
            get => pinClicked;
            set => SetProperty(ref pinClicked, value);
        }

        bool dropDownDroped;
        public bool DropDownDroped
        {
            get => dropDownDroped;
            set => SetProperty(ref dropDownDroped, value);
        }

        private Rectangle rectangle;
        public Rectangle DropDownSize
        {
            get => rectangle;
            set => SetProperty(ref rectangle, value);
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
        string searchBarText;
        public string SearchBarText
        {
            get => searchBarText;
            set => SetProperty(ref searchBarText, value);
        }

        #endregion

        public ICommand OnMapClickedCommand => new Command<object>(MapClickedCommand);
        public ICommand OnSearchBarFocusedCommand => new Command<object>(SearchBarFocusedCommand);
        public ICommand OnMapFocusedCommand => new Command<object>(MapFocusedCommand);
        public ICommand OnSearchBarTypingCommand => new Command<object>(SearchBarTypingCommand);
        public ICommand OnVisibleChangeCommand => new Command<object>(VisibleChangeCommand);
        public ICommand OnMarkClickedCommand => new Command<MyCustomPin>(MarkClickedCommand);
        public ICommand OnItemTappedCommand => new Command<PlaceViewModel>(ItemTappedCommand);
        public DelegateCommand OnUpdateMap => new DelegateCommand(OnUpdateMapCommand);

        #endregion

        #region -- Iterface implementations --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            PlaceViewModel SelectedPlace;
            if (parameters.TryGetValue(nameof(SelectedPlace), out SelectedPlace))
            {
                Random rand = new Random();
                MapSpan = new MapSpan(SelectedPlace.Position, 1 + rand.NextDouble() / 100, 1 + rand.NextDouble() / 100);
            }
            if (parameters.TryGetValue<Uri>("Uri", out var uri))
            {
                userService.AddUser(new Model.Tables.User.UserViewModel());
                PlaceService.AddPlace(placeSharingService.ParsingSharingPin(uri));
                OnUpdateMap.Execute();
                MapSpan = new MapSpan(Pins.First().Position, 1, 1);
            }
        }

        public override void OnAppearing()
        {
            OnUpdateMap?.Execute();
        }
        public override void OnDisappearing()
        {
            base.OnDisappearing();

            DropDownDroped = false;
            PinClicked = false;
        }

        #endregion

        #region -- Private helpers -- 
        private void MapFocusedCommand(object obj)
        {
            DropDownDroped = false;
        }

        private void OnUpdateMapCommand()
        {
            Pins = new ObservableCollection<PlaceViewModel>(PlaceService.GetUserPlaces().Where(u => u.Favorite));
            DropDownSize = new Rectangle(0, 100, 1, 0.06 + 0.07 * (Pins.Count > 5 ? 5 : Pins.Count));
            ClusteringPins = new ObservableCollection<PlaceViewModel>(Pins);
        }

        private void ItemTappedCommand(PlaceViewModel obj)
        {
            Random rand = new Random();
            MapSpan = new MapSpan(obj.Position, 1 + rand.NextDouble() / 100, 1 + rand.NextDouble() / 100);
        }
        
        private void MarkClickedCommand(MyCustomPin obj)
        {
            if (obj.ClusteringCount == 1)
            {
                PinClicked = true;
                MarkDescription = obj.Address;
                MarkName = obj.Label;
                MarkLatitude = obj.Position.Latitude.ToString();
                MarkLongitude = obj.Position.Longitude.ToString();
            }
        }

        private void VisibleChangeCommand(object obj)
        {
            var dsitanse = Math.Abs(((obj as MapSpan).Radius.Meters - OldMapCamera.Radius.Meters));
            if (dsitanse > 10)
            {

                OldMapCamera = new MapSpan((obj as MapSpan).Center, (obj as MapSpan).LatitudeDegrees, (obj as MapSpan).LongitudeDegrees);
                var places = new ObservableCollection<PlaceViewModel>(Clustering(Pins, (obj as MapSpan)));
                if (places.Count != ClusteringPins.Count)
                {
                    ClusteringPins = places;
                }
            }
        }
        private void SearchBarTypingCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(searchBarText))
            {
                OnUpdateMap?.Execute();
            }
            else
            {
                Pins = new ObservableCollection<PlaceViewModel>(Pins.Where(u => u.PlaceName.Contains(SearchBarText)));
                DropDownSize = new Rectangle(0, 100, 1, 0.06 + 0.07 * (Pins.Count > 5 ? 5 : Pins.Count));
            }
        }
        private void SearchBarFocusedCommand(object obj)
        {
            DropDownDroped = true;
        }

        private void MapClickedCommand(object obj)
        {
            DropDownDroped = false;
            PinClicked = false;
        }

        private IEnumerable<PlaceViewModel> Clustering(IEnumerable<PlaceViewModel> collection, MapSpan CameraDistance)
        {
            var newCollection = new List<PlaceViewModel>(collection.Where(u => true));

            for (int i = 0; i < newCollection.Count; i++)
            {
                for (int j = i + 1; j < newCollection.Count; j++)
                {
                    if (j <= newCollection.Count)
                    {
                        if (Distance.BetweenPositions(newCollection[i].Position, newCollection[j].Position).Meters < CameraDistance.Radius.Meters / 10)
                        {
                            Xamarin.Forms.Maps.Position position = new Xamarin.Forms.Maps.Position
                                (
                                    (newCollection[i].Position.Latitude + newCollection[j].Position.Latitude) / 2,
                                    (newCollection[i].Position.Longitude + newCollection[j].Position.Longitude) / 2
                                );

                            PlaceViewModel newpin = new PlaceViewModel()
                            {
                                ClusteringCount = newCollection[i].ClusteringCount + newCollection[j].ClusteringCount,
                                PlaceName = (newCollection[i].ClusteringCount + newCollection[j].ClusteringCount).ToString(),
                                Position = position
                            };

                            newCollection.RemoveAt(j);
                            newCollection.RemoveAt(i);
                            newCollection.Add(newpin);
                        }
                    }
                    if (newCollection.Count != collection.Count())
                    {
                        break;
                    }
                }
                if (newCollection.Count != collection.Count())
                {
                    break;
                }
            }
            if (newCollection.Count != collection.Count())
            {
                newCollection = Clustering(newCollection, CameraDistance).ToList();
            }
            return newCollection;
        }

        #endregion
    
    }
}

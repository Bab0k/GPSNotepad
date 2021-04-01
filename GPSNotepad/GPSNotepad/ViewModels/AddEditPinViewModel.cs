using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GPSNotepad.ViewModels
{
    public class AddEditPinViewModel : ViewModelBase, INavigationAware
    {
        IRepository repository = new RealmRepository();

        private string PlaceId;
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

        double lantitude;
        public string Latitude
        {
            get => lantitude.ToString();
            set
            {
                double lant;

                if (double.TryParse(value, out lant))
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
                double lng;

                if (double.TryParse(value, out lng))
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
        Map map = new Map();
        public Map Map
        {
            get => map;
            set => SetProperty(ref map, value);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == "Latitude")
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
            if (args.PropertyName == "Longitude")
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



            if (args.PropertyName == "Position" || args.PropertyName == "Description")
            {
                Pin pin = new Pin()
                {
                    Position = Position,
                    Label = Description,

                };

                map.Pins.Clear();
                map.Pins.Add(pin);

                map.MoveToRegion(new MapSpan(Position, 1, 1));

            }

        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            BasePlace place = new BasePlace()
            {
                PlaceId = PlaceId,
                PlaceName = Name,
                Position = Position,
                Address = Description
            };

            repository.EditPlace(place);

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("PlaceId"))
            {
                PlaceId = parameters.GetValues<string>("PlaceId").First();
            }
        }

        public AddEditPinViewModel(INavigationService navigationService) : base(navigationService)
        {
            Map.MapClicked += OnMapClick;
        }

        private void OnMapClick(object sender, MapClickedEventArgs e)
        {
            Longitude = e.Position.Longitude.ToString();
            Latitude = e.Position.Latitude.ToString();
        }

    }
}

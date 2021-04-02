using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace GPSNotepad.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        IRepository repository = new RealmRepository();

        ObservableCollection<BasePlace> pins = new ObservableCollection<BasePlace>();
        public ObservableCollection<BasePlace> Pins 
        {
            get => pins; 
            set => SetProperty(ref pins, value); 
        }
        MapSpan mapSpan = new MapSpan(new Xamarin.Forms.Maps.Position(10,10),1,1);
        public MapSpan MapSpan
        {
            get => mapSpan;
            set => SetProperty(ref mapSpan, value);
        }

        public MapViewModel(INavigationService navigationService) : base(navigationService)
        {
            Pins = repository.GetPlaces();
        }

        public DelegateCommand OnUpdateMap =>
          new DelegateCommand(UpdateMapCommand);

        private void UpdateMapCommand()
        {
            Pins = repository.GetPlaces();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("SelectedPlace"))
            {   

            }
        }
    }
}

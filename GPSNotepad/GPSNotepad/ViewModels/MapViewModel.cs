using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    }
}

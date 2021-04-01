using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using GPSNotepad.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GPSNotepad.ViewModels
{
    public class PinListViewModel : ViewModelBase, INavigatedAware
    {

        IRepository repository = new RealmRepository();

        private ObservableCollection<BasePlace> pins = new ObservableCollection<BasePlace>();
        public ObservableCollection<BasePlace> Pins
        {
            get => pins;
            set => SetProperty(ref pins, value);
        }

        public PinListViewModel(INavigationService navigationService) : base(navigationService)
        {
            Pins = repository.GetPlaces();
        }


        public DelegateCommand OnNavigationToAddEditPinView =>
          new DelegateCommand(NavigationToAddEditPinViewCommand);

        string AddNewPlace()
        {
            var place = new BasePlace();
            repository.AddPlace(place);
            return place.PlaceId;
        }

        private void NavigationToAddEditPinViewCommand()
        {
            NavigationParameters pairs = new NavigationParameters();

            pairs.Add("PlaceId", AddNewPlace());

            NavigationService.NavigateAsync(nameof(AddEditPinView), pairs);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Pins = repository.GetPlaces();

            
        }
    }
}

using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using GPSNotepad.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Navigation.TabbedPages;

namespace GPSNotepad.ViewModels
{
    public class PinListViewModel : ViewModelBase, INavigatedAware
    {

        private IRepository repository;

        private ObservableCollection<BasePlace> pins = new ObservableCollection<BasePlace>();
        public ObservableCollection<BasePlace> Pins
        {
            get => pins;
            set => SetProperty(ref pins, value);
        }
        private BasePlace selectedItem;
        public BasePlace SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public PinListViewModel(INavigationService navigationService, IRepository repository) : base(navigationService)
        {
            this.repository = repository;
            Pins = repository.GetPlaces();
        }


        public DelegateCommand OnNavigationToAddEditPinView =>
          new DelegateCommand(NavigationToAddEditPinViewCommand);
        public ICommand OnEditCommand => new DelegateCommand<BasePlace>(EditCommand);

        public ICommand OnDeleteCommand => new DelegateCommand<BasePlace>(DeleteCommand);
        public ICommand OnChangeTabCommand => new DelegateCommand<BasePlace>(ChangeTabCommand);

        private void ChangeTabCommand(BasePlace obj)
        {
            var Params = new NavigationParameters();

            Params.Add("SelectedPlace", obj);

            NavigationService.SelectTabAsync($"{nameof(MapView)}", Params);
        }

        private void DeleteCommand(BasePlace e)
        {
            repository.RemovePlace(e.PlaceId);

            UpdatePins();
        }

        private void EditCommand(BasePlace e)
        {
            NavigationParameters pairs = new NavigationParameters();

            pairs.Add("Place", e);

            NavigationService.NavigateAsync(nameof(AddEditPinView), pairs);
        }

        private void UpdatePins()
        {
            Pins = repository.GetPlaces();
        }

        BasePlace AddNewPlace()
        {
            var place = new BasePlace();
            repository.AddPlace(place);
            return place;
        }

        private async void NavigationToAddEditPinViewCommand()
        {
            NavigationParameters pairs = new NavigationParameters();

            pairs.Add("Place", AddNewPlace());

            await NavigationService.NavigateAsync(nameof(AddEditPinView), pairs);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            UpdatePins();
        }
    }
}

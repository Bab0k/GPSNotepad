using GPSNotepad.Model.Tables;
using GPSNotepad.Repository;
using GPSNotepad.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Navigation.TabbedPages;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using GPSNotepad.Controls;
using GPSNotepad.Servises.PlaceService;

namespace GPSNotepad.ViewModels
{
    public class PinListViewModel : ViewModelBase, INavigatedAware
    {

        private IPlaceService PlaceService;

        private ObservableCollection<PlaceViewModel> pins = new ObservableCollection<PlaceViewModel>();
        public ObservableCollection<PlaceViewModel> Pins
        {
            get => pins;
            set => SetProperty(ref pins, value);
        }
        private PlaceViewModel selectedItem;
        public PlaceViewModel SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public PinListViewModel(INavigationService navigationService, IPlaceService PlaceService) : base(navigationService)
        {
            this.PlaceService = PlaceService;
        }


        public DelegateCommand OnNavigationToAddEditPinView =>
          new DelegateCommand(NavigationToAddEditPinViewCommand);
        public ICommand OnEditCommand => new DelegateCommand<PlaceViewModel>(EditCommand);

        public ICommand OnDeleteCommand => new DelegateCommand<PlaceViewModel>(DeleteCommand);
        public ICommand OnChangeTabCommand => new DelegateCommand<Object>(ChangeTabCommand);

        private void ChangeTabCommand(Object obj)
        {
            var Params = new NavigationParameters();

            Params.Add("SelectedPlace", (obj as PinListViewModel).SelectedItem);

            NavigationService.SelectTabAsync($"{nameof(MapView)}", Params);
        }

        public ICommand OnCheckedChangeCommand => new DelegateCommand<object>(CheckedChangeCommand);
        private void CheckedChangeCommand(object obj)
        {
            var control = (obj as MyCustomCheckBox);
            control.IsFavorite = control.IsChecked;
            UpdateFavoriteProperty();
        }

        private void DeleteCommand(PlaceViewModel e)
        {
            PlaceService.RemovePlace(e);

            UpdatePins();
        }

        private void EditCommand(PlaceViewModel _place)
        {
            NavigationParameters pairs = new NavigationParameters() 
            {
                { nameof(_place), _place } 
            };
            
            NavigationService.NavigateAsync(nameof(AddEditPinView), pairs);
        }

        private void UpdatePins()
        {
            Pins = PlaceService.GetUserPlaces();
        }



        protected void UpdateFavoriteProperty()
        {
            foreach (var item in Pins)
            {
                PlaceService.EditPlace(item);
            }
        }

        private async void NavigationToAddEditPinViewCommand()
        {
            await NavigationService.NavigateAsync(nameof(AddEditPinView));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            UpdatePins();
        }
    }
}

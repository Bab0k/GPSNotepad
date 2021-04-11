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
using Xamarin.Forms;
using System.Linq;
using GPSNotepad.Servises.PlaceSharingService;

namespace GPSNotepad.ViewModels
{
    public class PinListViewModel : ViewModelBase, INavigatedAware
    {

        #region -- Private Variables/Properties -- 

        private readonly IPlaceService PlaceService;
        private readonly IPlaceSharingService placeSharingService;

        #endregion

        #region -- Constructors --

        public PinListViewModel(INavigationService navigationService, IPlaceService PlaceService, IPlaceSharingService placeSharingService) : base(navigationService)
        {
            this.PlaceService = PlaceService;
            this.placeSharingService = placeSharingService;
        }

        #endregion

        #region -- Public properties --

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
        string searchBarText;
        public string SearchBarText
        {
            get => searchBarText;
            set => SetProperty(ref searchBarText, value);
        }

        public ICommand OnNavigationToAddEditPinView  => new DelegateCommand(NavigationToAddEditPinViewCommand);
        public ICommand OnEditCommand => new DelegateCommand<PlaceViewModel>(EditCommand);
        public ICommand OnDeleteCommand => new DelegateCommand<PlaceViewModel>(DeleteCommand);
        public ICommand OnSharingCommand => new DelegateCommand<PlaceViewModel>(SharingCommand);
        public ICommand OnChangeTabCommand => new DelegateCommand<Object>(ChangeTabCommand);
        public ICommand OnSearchBarTypingCommand => new DelegateCommand<object>(SearchBarTypingCommand);
        public ICommand OnCheckedChangeCommand => new DelegateCommand<object>(CheckedChangeCommand);

        #endregion

        #region -- overrides -- 

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            UpdatePins();
        }

        #endregion

        #region -- Protected implementation --

        protected void UpdateFavoriteProperty()
        {
            foreach (var item in Pins)
            {
                PlaceService.EditPlace(item);
            }
        }

        #endregion

        #region -- Private helpers --

        private void SharingCommand(PlaceViewModel obj)
        {
            placeSharingService.SendPinAsync(obj);
        }

        private void SearchBarTypingCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(searchBarText))
            {
                UpdatePins();
            }
            else
            {
                Pins = new ObservableCollection<PlaceViewModel>(Pins.Where(u => u.PlaceName.Contains(SearchBarText)));
            }
        }

        private void ChangeTabCommand(Object obj)
        {
            var SelectedPlace = (obj as PinListViewModel).SelectedItem;

            var Params = new NavigationParameters
            {
                { nameof(SelectedPlace),  SelectedPlace}
            };

            NavigationService.SelectTabAsync($"{nameof(MapView)}", Params);
        }

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

        private async void NavigationToAddEditPinViewCommand()
        {
            await NavigationService.NavigateAsync(nameof(AddEditPinView));
        }

        #endregion
    }
}

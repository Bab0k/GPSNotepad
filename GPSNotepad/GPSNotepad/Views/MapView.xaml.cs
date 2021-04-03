using Xamarin.Forms;

namespace GPSNotepad.Views
{
    public partial class MapView : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as ViewModels.MapViewModel).OnUpdateMap.Execute();
        }

        public MapView()
        {
            InitializeComponent();
        }
    }
}

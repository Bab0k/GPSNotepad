using GPSNotepad.Model.Interface;
using Xamarin.Forms;

namespace GPSNotepad.Views
{
    public class BaseContentPage: ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is IViewActionsHandler actionsHandler)
            {
                actionsHandler.OnAppearing();
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is IViewActionsHandler actionsHandler)
            {
                actionsHandler.OnDisappearing();
            }
        }

    }
}

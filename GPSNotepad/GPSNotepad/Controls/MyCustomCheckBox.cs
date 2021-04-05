using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public class MyCustomCheckBox: Frame
    {
        public bool IsFavorite
        {
            get { return (bool)GetValue(IsFavotiteProperty); }
            set { SetValue(IsFavotiteProperty, value); }
        }

        public static readonly BindableProperty IsFavotiteProperty = BindableProperty.Create(
                                                         propertyName: "IsFavorite",
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(MyCustomCheckBox),
                                                         defaultValue: false,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: IsFavoritePropertyChanged);

        private static void IsFavoritePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as MyCustomCheckBox;
            var newMapSpan = newValue;

            Image image = new Image();

            image.Source = (bool)newValue ? "Ok.png":"";

            thisInstance.Content = image;

        }
    }
}

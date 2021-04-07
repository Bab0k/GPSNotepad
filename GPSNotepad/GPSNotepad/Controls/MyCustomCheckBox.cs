using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public class MyCustomCheckBox: CheckBox
    {
        public bool IsFavorite
        {
            get { return (bool)GetValue(IsFavotiteProperty); }
            set { SetValue(IsFavotiteProperty, value); }
        }

        public static readonly BindableProperty IsFavotiteProperty = 
            BindableProperty.Create(
                   propertyName: "IsFavorite",
                   returnType: typeof(bool),
                   declaringType: typeof(MyCustomCheckBox),
                   defaultValue: false,
                   defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty CheckedChangeCommandProperty =
            BindableProperty.Create(
                  propertyName: nameof(CheckedChangeCommand),
                  returnType: typeof(ICommand),
                  declaringType: typeof(CustomMap),
                  defaultValue: default(ICommand));

        public ICommand CheckedChangeCommand
        {
            get { return (ICommand)GetValue(CheckedChangeCommandProperty); }
            set { SetValue(CheckedChangeCommandProperty, value); }
        }

        public MyCustomCheckBox():base()
        {
            CheckedChanged += MyCustomCheckBox_CheckedChanged;
        }

        private void MyCustomCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckedChangeCommand?.Execute(sender);
        }
    }
}

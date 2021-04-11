using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public class MyCustomCheckBox: CheckBox
    {
        #region -- Public static properties --

        public static readonly BindableProperty IsFavotiteProperty = 
            BindableProperty.Create(
                   propertyName: nameof(IsFavorite),
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

        #endregion

        #region -- Constructors --

        public MyCustomCheckBox():base()
        {
            CheckedChanged += MyCustomCheckBox_CheckedChanged;
        }

        #endregion

        #region -- Public properties --

        public bool IsFavorite
        {
            get { return (bool)GetValue(IsFavotiteProperty); }
            set { SetValue(IsFavotiteProperty, value); }
        }

        public ICommand CheckedChangeCommand
        {
            get { return (ICommand)GetValue(CheckedChangeCommandProperty); }
            set { SetValue(CheckedChangeCommandProperty, value); }
        }

        #endregion

        #region -- Private helpers --

        private void MyCustomCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckedChangeCommand?.Execute(sender);
        }

        #endregion
    }
}

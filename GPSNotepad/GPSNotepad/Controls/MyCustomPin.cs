using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GPSNotepad.Controls
{
    public class MyCustomPin : Pin
    {
        #region -- Public static properties --

        public static readonly BindableProperty MarkClickCommandProperty =
             BindableProperty.Create(
                 propertyName: nameof(MarkClickCommand),
                 returnType: typeof(ICommand),
                 declaringType: typeof(CustomMap),
                 defaultValue: default(ICommand));

        public static readonly BindableProperty ClusteringCountProperty =
             BindableProperty.Create(
                 propertyName: nameof(ClusteringCount),
                 returnType: typeof(int),
                 declaringType: typeof(CustomMap),
                 defaultValue: default(int));

        #endregion

        #region -- Public properties --

        public int ClusteringCount
        {
            get { return (int)GetValue(ClusteringCountProperty); }
            set { SetValue(ClusteringCountProperty, value); }
        }

        public ICommand MarkClickCommand
        {
            get { return (ICommand)GetValue(MarkClickCommandProperty); }
            set { SetValue(MarkClickCommandProperty, value); }
        }

        #endregion

        #region -- Constructors --
        public MyCustomPin() : base()
        {
            this.MarkerClicked += MyCustomPin_MarkerClicked;
        }

        #endregion

        #region -- Private helpers --

        private void MyCustomPin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            MarkClickCommand?.Execute(sender);
        }

        #endregion

    }
}

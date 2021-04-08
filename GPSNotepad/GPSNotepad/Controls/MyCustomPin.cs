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
        public MyCustomPin() : base()
        {
            this.MarkerClicked += MyCustomPin_MarkerClicked;
        }

        public int ClusteringCount
        {
            get { return (int)GetValue(ClusteringCountProperty); }
            set { SetValue(ClusteringCountProperty, value); }
        }
        private void MyCustomPin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            MarkClickCommand?.Execute(sender);
        }
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


        public ICommand MarkClickCommand
        {
            get { return (ICommand)GetValue(MarkClickCommandProperty); }
            set { SetValue(MarkClickCommandProperty, value); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GPSNotepad.Controls
{
    public class CustomMap : Map
    {
        public CustomMap()
        {
            PinsSource = new ObservableCollection<MyCustomPin>();
            PinsSource.CollectionChanged += PinsSourceOnCollectionChanged;
            MapClicked += CustomMap_MapClicked;
        }

        private void CustomMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            MapClickedCommand?.Execute(e);
        }

        public ObservableCollection<MyCustomPin> PinsSource
        {
            get { return (ObservableCollection<MyCustomPin>)GetValue(PinsSourceProperty); }
            set { SetValue(PinsSourceProperty, value); }
        }

        public static readonly BindableProperty PinsSourceProperty = BindableProperty.Create(
                                                         propertyName: "PinsSource",
                                                         returnType: typeof(ObservableCollection<MyCustomPin>),
                                                         declaringType: typeof(CustomMap),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         validateValue: null,
                                                         propertyChanged: PinsSourcePropertyChanged);

        public MapSpan MapSpan
        {
            get { return (MapSpan)GetValue(MapSpanProperty); }
            set { SetValue(MapSpanProperty, value); }
        }

        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(
                                                         propertyName: "MapSpan",
                                                         returnType: typeof(MapSpan),
                                                         declaringType: typeof(CustomMap),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         validateValue: null,
                                                         propertyChanged: MapSpanPropertyChanged);

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(VisibleRegion))
            {
                VisibleChangeCommand?.Execute(VisibleRegion);
            }
        }

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as CustomMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }

        private static void PinsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            if (!(bindable is CustomMap thisInstance) ||
                !(newValue is ObservableCollection<MyCustomPin> newPinsSource))
                return;

            UpdatePinsSource(thisInstance, newPinsSource);
        }

        private void PinsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePinsSource(this, sender as IEnumerable<MyCustomPin>);

        }

        private static void UpdatePinsSource(Map bindableMap, IEnumerable<MyCustomPin> newSource)
        {
            bindableMap.Pins.Clear();
            foreach (var pin in newSource)
                bindableMap.Pins.Add(pin);
        }

        public static readonly BindableProperty MapClickedCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(MapClickedCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(CustomMap),
                defaultValue: default(ICommand));

        public ICommand MapClickedCommand
        {
            get { return (ICommand)GetValue(MapClickedCommandProperty); }
            set { SetValue(MapClickedCommandProperty, value); }
        }

        public static readonly BindableProperty VisibleChangeCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(VisibleChangeCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(CustomMap),
                defaultValue: default(ICommand));

        public ICommand VisibleChangeCommand
        {
            get { return (ICommand)GetValue(VisibleChangeCommandProperty); }
            set { SetValue(VisibleChangeCommandProperty, value); }
        }
    }
}

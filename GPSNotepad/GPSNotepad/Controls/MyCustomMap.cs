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
        #region -- Public static properties --

        public static readonly BindableProperty PinsSourceProperty = BindableProperty.Create(
                                                         propertyName: nameof(PinsSource),
                                                         returnType: typeof(ObservableCollection<MyCustomPin>),
                                                         declaringType: typeof(CustomMap),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         validateValue: null,
                                                         propertyChanged: PinsSourcePropertyChanged);

        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(
                                                         propertyName: nameof(MapSpan),
                                                         returnType: typeof(MapSpan),
                                                         declaringType: typeof(CustomMap),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         validateValue: null,
                                                         propertyChanged: MapSpanPropertyChanged);

        public static readonly BindableProperty VisibleChangeCommandProperty = BindableProperty.Create(
                                                         propertyName: nameof(VisibleChangeCommand),
                                                         returnType: typeof(ICommand),
                                                         declaringType: typeof(CustomMap),
                                                         defaultValue: default(ICommand));

        public static readonly BindableProperty MapClickedCommandProperty = BindableProperty.Create(
                                                         propertyName: nameof(MapClickedCommand),
                                                         returnType: typeof(ICommand),
                                                         declaringType: typeof(CustomMap),
                                                         defaultValue: default(ICommand));
        #endregion

        #region -- Constructors --

        public CustomMap()
        {
            PinsSource = new ObservableCollection<MyCustomPin>();
            PinsSource.CollectionChanged += PinsSourceOnCollectionChanged;
            MapClicked += CustomMap_MapClicked;
        }

        #endregion

        #region -- Public properties --

        public ObservableCollection<MyCustomPin> PinsSource
        {
            get { return (ObservableCollection<MyCustomPin>)GetValue(PinsSourceProperty); }
            set { SetValue(PinsSourceProperty, value); }
        }

        public MapSpan MapSpan
        {
            get { return (MapSpan)GetValue(MapSpanProperty); }
            set { SetValue(MapSpanProperty, value); }
        }

        public ICommand MapClickedCommand
        {
            get { return (ICommand)GetValue(MapClickedCommandProperty); }
            set { SetValue(MapClickedCommandProperty, value); }
        }

        public ICommand VisibleChangeCommand
        {
            get { return (ICommand)GetValue(VisibleChangeCommandProperty); }
            set { SetValue(VisibleChangeCommandProperty, value); }
        }

        #endregion

        #region -- Protected implementation --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(VisibleRegion))
            {
                VisibleChangeCommand?.Execute(VisibleRegion);
            }
        }

        #endregion

        #region -- Private helpers --

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as CustomMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }

        private static void PinsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            if ((bindable is CustomMap thisInstance) &&
                (newValue is ObservableCollection<MyCustomPin> newPinsSource))
            {
                UpdatePinsSource(thisInstance, newPinsSource);
            }
        }

        private static void UpdatePinsSource(Map bindableMap, IEnumerable<MyCustomPin> newSource)
        {
            bindableMap.Pins.Clear();
            foreach (var pin in newSource)
                bindableMap.Pins.Add(pin);
        }

        private void CustomMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            MapClickedCommand?.Execute(e);
        }

        private void PinsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePinsSource(this, sender as IEnumerable<MyCustomPin>);
        }

        #endregion
    }
}

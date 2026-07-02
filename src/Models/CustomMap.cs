using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace MapTracker.Models
{
    public class CustomMap : Microsoft.Maui.Controls.Maps.Map
    {
        public static readonly BindableProperty RoutePathProperty =
        BindableProperty.Create(
            nameof(RoutePath),
            typeof(List<(double Lat, double Lng)>),
            typeof(CustomMap),
            null,
            propertyChanged: OnRoutePathChanged);

        public List<(double Lat, double Lng)> RoutePath
        {
            get => (List<(double Lat, double Lng)>)GetValue(RoutePathProperty);
            set => SetValue(RoutePathProperty, value);
        }

        public static readonly BindableProperty CurrentPositionProperty =
            BindableProperty.Create(
                nameof(CurrentPosition),
                typeof((double Lat, double Lng)),
                typeof(CustomMap),
                default((double, double)),
                propertyChanged: OnCurrentPositionChanged);

        public (double Lat, double Lng) CurrentPosition
        {
            get => ((double Lat, double Lng))GetValue(CurrentPositionProperty);
            set => SetValue(CurrentPositionProperty, value);
        }

        private Polyline? _routeLine;
        private Pin? _progressPin;

        private static void OnRoutePathChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CustomMap)bindable).DrawRoute();
        }

        private static void OnCurrentPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CustomMap)bindable).UpdateMarker();
        }

        private void DrawRoute()
        {
            if (RoutePath == null || RoutePath.Count == 0)
                return;

            _routeLine = new Polyline
            {
                StrokeColor = Colors.Red,
                StrokeWidth = 6
            };

            foreach (var p in RoutePath)
                _routeLine.Geopath.Add(new Location(p.Lat, p.Lng));

            MapElements.Clear();
            MapElements.Add(_routeLine);
        }

        private void UpdateMarker()
        {
            var (lat, lng) = CurrentPosition;

            if (_progressPin == null)
            {
                _progressPin = new Pin
                {
                    Label = "Progress",
                    Location = new Location(lat, lng)
                };
                Pins.Add(_progressPin);
            }
            else
            {
                _progressPin.Location = new Location(lat, lng);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using MapTracker.Models;


namespace MapTracker.ViewModels
{
    public partial class MapViewModel : ObservableObject
    {
        public Route Route { get; }
        [ObservableProperty]
        private double distanceWalked;

        [ObservableProperty]
        private (double Lat, double Lng) currentPosition;

        public MapViewModel(Route route)
        {
            Route = route;
        }

        private void UpdateProgressMarker()
        {
            double progress = DistanceWalked / Route.TotalDistance;
            CurrentPosition = Route.Position(progress);


        }
    }
}

using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MapTracker.Models;

namespace MapTracker.ViewModels;

public partial class LocationViewModel : ObservableObject
{
    public LocationViewModel()
    {
        GetCurrentLocationCommand = new Command(GetCurrentLocation, CanGetCurrentLocation);
    }

    [ObservableProperty]
    public partial double CurrLatitude {get; set;} = 0;
    [ObservableProperty]
    public partial double CurrLongitude {get; set;} = 0;


    public CurrLocation location = new();

    private async void GetCurrentLocation(object obj)
    {
        location.GetCurrent();
        CurrLatitude = location.Latitude;
        CurrLongitude = location.Longitude;

        Console.WriteLine("Location: {0} {1}", CurrLatitude, CurrLongitude);
    }
    
    public ICommand GetCurrentLocationCommand {get; set;}
    private bool CanGetCurrentLocation(object arg) => true;
}

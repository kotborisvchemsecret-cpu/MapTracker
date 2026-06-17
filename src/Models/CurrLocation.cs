namespace MapTracker.Models;

public class CurrLocation
{
    public double Latitude {get; private set;} = 0;
    public double Longitude {get; private set;} = 0;

    private CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();

    public async void GetCurrent()
    {
        try
        {
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
            Location? location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
            {
                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledExceptionW
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
            Console.WriteLine("Exception: {0}", ex.Message);
        }
    }

    public void CancelRequest()
    {
        if (_cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }
}

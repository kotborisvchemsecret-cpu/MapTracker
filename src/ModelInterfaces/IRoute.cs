using MapTracker.Models;

namespace MapTracker.ModelInterfaces
{
    public interface IRoute
    {

        //* Calculates the position (coordinates) along the route based on a progress value between 0 and 1.
        Coordinate Position(double progress);
    }
}
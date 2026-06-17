using System;
using System.Collections.Generic;
using System.Text;

namespace MapTracker.Models
{
    public class Route
    {
        public List<Coordinate> Path { get; init; } = new();

        private double? _totalDistance;

        public double TotalDistance =>
            _totalDistance ??= CalculateTotalDistance();

        private double CalculateTotalDistance()
        {
            double total = 0;

            for (int i = 1; i < Path.Count; i++)
            {
                total += Distance(Path[i - 1], Path[i]);
            }

            return total;
        }

        //* Calculates the position (coordinates) along the route based on a progress value between 0 and 1.
        public Coordinate Position(double progress)
        {
            if (Path.Count == 0)
                throw new InvalidOperationException("Path is empty.");

            if (Path.Count == 1)
                return Path[0];

            progress = Math.Clamp(progress, 0, 1);

            double targetDistance = TotalDistance * progress;
            double accumulatedDistance = 0;

            for (int i = 1; i < Path.Count; i++)
            {
                Coordinate start = Path[i - 1];
                Coordinate end = Path[i];

                double segmentDistance = Distance(start, end);

                if (segmentDistance == 0)
                    continue;

                if (accumulatedDistance + segmentDistance >= targetDistance)
                {
                    double distanceIntoSegment =
                        targetDistance - accumulatedDistance;

                    double ratio =
                        distanceIntoSegment / segmentDistance;

                    return new Coordinate
                    {
                        Latitude = start.Latitude +
                                   ratio * (end.Latitude - start.Latitude),

                        Longitude = start.Longitude +
                                    ratio * (end.Longitude - start.Longitude)
                    };
                }

                accumulatedDistance += segmentDistance;
            }

            return Path[^1];
        }

        private static double Distance(
            Coordinate a,
            Coordinate b)
        {
            double dx = b.Longitude - a.Longitude;
            double dy = b.Latitude - a.Latitude;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}

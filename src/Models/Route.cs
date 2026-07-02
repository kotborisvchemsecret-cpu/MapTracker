using System;
using System.Collections.Generic;
using System.Text;

namespace MapTracker.Models
{
    public class Route
    {
        public List<(double Lat, double Lng)> Path { get; }

        private double? _totalDistance;

        public Route(List<(double Lat, double Lng)> path)
        {
            Path = path;
        }

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

        public (double Lat, double Lng) Position(double progress)
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
                (double Lat, double Lng) start = Path[i - 1];
                (double Lat, double Lng) end = Path[i];

                double segmentDistance = Distance(start, end);

                if (segmentDistance == 0)
                    continue;

                if (accumulatedDistance + segmentDistance >= targetDistance)
                {
                    double distanceIntoSegment =
                        targetDistance - accumulatedDistance;

                    double ratio =
                        distanceIntoSegment / segmentDistance;

                    return new(start.Lat +
                                   ratio * (end.Lat - start.Lat), start.Lng +
                                    ratio * (end.Lng - start.Lng));
                }

                accumulatedDistance += segmentDistance;
            }

            return Path[^1];
        }

        private static double Distance(
            (double Lat, double Lng) a,
            (double Lat, double Lng) b)
        {
            double dx = b.Lng - a.Lng;
            double dy = b.Lat - a.Lat;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}

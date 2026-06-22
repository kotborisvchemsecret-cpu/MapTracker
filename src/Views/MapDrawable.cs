using MapTracker.Models;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapTracker.Views
{
    public class MapDrawable : IDrawable
    {
        public List<Coordinate> Route { get; set; }
        public Coordinate Marker { get; set; }
        private readonly Microsoft.Maui.Graphics.IImage _map;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.DrawImage(
            _map,
            0,
            0,
            dirtyRect.Width,
            dirtyRect.Height);
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 4;
            for (int i = 1; i < Route.Count; i++)
            {
                canvas.DrawLine(
                    (float)Route[i - 1].Latitude,
                    (float)Route[i - 1].Longitude,
                    (float)Route[i].Latitude,
                    (float)Route[i].Longitude);
            }
            canvas.FillColor = Colors.Blue;

            canvas.FillCircle(
                (float)Marker.Latitude,
                (float)Marker.Longitude,
                10);
        }

        public MapDrawable(List<Coordinate> route, Coordinate marker)
        {
            Route = route;
            Marker = marker;
            using var stream =
            FileSystem.OpenAppPackageFileAsync(
                "middleearth.png")
                .Result;

            _map = PlatformImage.FromStream(stream);
        }
    }
}

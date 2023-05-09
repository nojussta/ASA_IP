using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    internal class Location
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Location(string name, long id, double x, double y)
        {
            Name = name;
            Id = id;
            X = x;
            Y = y;
        }

        public double DistanceTo(Location other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}

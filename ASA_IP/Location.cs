using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    public class Location
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Location(long id, string name, double x, double y)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
        }

        public double DistanceTo(Location other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        public static Location CreateLocation(string line)
        {
            var values = line.Split(';');
            string name = values[0];
            long id = long.Parse(values[1]);
            var x = double.Parse(values[2]);
            var y = double.Parse(values[3]);

            return new Location(id, name, x, y);
        }
    }
}

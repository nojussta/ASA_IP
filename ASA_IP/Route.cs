using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    internal class Route
    {
        public List<Location> VisitedLocations { get; set; }
        public double Quality;

        public Route()
        {
            VisitedLocations = new List<Location>();
            this.Quality = 0;
        }

        public double GetTotalDistance()
        {
            double totalDistance = 0;
            for (int i = 0; i < VisitedLocations.Count - 1; i++)
            {
                totalDistance += VisitedLocations[i].DistanceTo(VisitedLocations[i + 1]);
            }
            return totalDistance;
        }

        public bool ContainsLocation(Location Location)
        {
            return VisitedLocations.Any(p => p.Id == Location.Id);
        }

        public void AddLocation(Location Location)
        {
            VisitedLocations.Add(Location);
        }
    }
}

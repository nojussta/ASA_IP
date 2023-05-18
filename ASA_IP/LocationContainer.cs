using System.Collections.Generic;

namespace ASA_IP
{
    internal class LocationContainer
    {
        private readonly List<Location> allLocations;

        public LocationContainer()
        {
            allLocations = new List<Location>();
        }

        public void AddLocation(long id, string name, double x, double y)
        {
            allLocations.Add(new Location(id, name, x, y));
        }

        public void AddLocation(Location Location)
        {
            allLocations.Add(Location);
        }

        public List<Location> GetAllLocations()
        {
            return allLocations;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "Duomenys.csv";
            List<Location> locations = InOutUtils.ReadData(fileName);

            //Console.WriteLine(RoutePlanner.FindRoute(locations));
            var routes = RoutePlanner.GetPermutations(locations, locations.Count);

            var shortestRoute = new List<Location>();
            var shortestDistance = double.MaxValue;

            foreach (var route in routes)
            {
                var distance = RoutePlanner.CalculateRouteDistance(route);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestRoute = route;
                }
            }

            Console.WriteLine("Shortest route:");
            foreach (var location in shortestRoute)
            {
                Console.WriteLine(location.Name);
            }
            Console.WriteLine($"Total distance: {shortestDistance:F2}");
        }
    }
}

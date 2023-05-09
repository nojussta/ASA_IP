using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    internal class RoutePlanner
    {
        public static List<Location> FindRoute(List<Location> locations)
        {
            int numTravelers = 3;

            // Calculate distances between all pairs of locations
            double[,] distances = new double[locations.Count, locations.Count];
            for (int i = 0; i < locations.Count; i++)
            {
                for (int j = 0; j < locations.Count; j++)
                {
                    distances[i, j] = locations[i].DistanceTo(locations[j]);
                }
            }

            // Initialize variables for holding the best route and its cost
            List<Location> bestRoute = null;
            double bestCost = double.MaxValue;

            // Generate all permutations of the locations
            var permutations = GetPermutations(locations, numTravelers);

            // Find the cheapest route among all permutations
            foreach (var permutation in permutations)
            {
                double cost = 0;

                // Calculate the cost of this route
                for (int i = 0; i < permutation.Count - 1; i++)
                {
                    cost += Math.Sqrt(distances[permutation[i].Id, permutation[i + 1].Id]);
                }

                // If this is a cheaper route, update the best route and cost
                if (cost < bestCost)
                {
                    bestRoute = permutation;
                    bestCost = cost;
                }
            }

            return bestRoute;
        }

        public static List<List<Location>> GetPermutations<Location>(List<Location> list, int length)
        {
            var results = new List<List<Location>>();

            if (length == 1)
            {
                foreach (var item in list)
                {
                    results.Add(new List<Location> { item });
                }
            }
            else
            {
                foreach (var item in list)
                {
                    var remainingList = new List<Location>(list);
                    remainingList.Remove(item);

                    foreach (var permutation in GetPermutations(remainingList, length - 1))
                    {
                        permutation.Insert(0, item);
                        results.Add(permutation);
                    }
                }
            }

            return results;
        }

        public static double CalculateRouteDistance(List<Location> locations)
        {
            double totalDistance = 0;
            for (int i = 0; i < locations.Count - 1; i++)
            {
                double distance = CalculateDistanceBetweenPoints(locations[i].X, locations[i].Y,
                                                                  locations[i + 1].X, locations[i + 1].Y);
                totalDistance += distance;
            }
            // Add distance back to the starting location to complete the route
            double finalDistance = CalculateDistanceBetweenPoints(locations[locations.Count - 1].X,
                                                                   locations[locations.Count - 1].Y,
                                                                   locations[0].X, locations[0].Y);
            totalDistance += finalDistance;
            return totalDistance;
        }

        public static double CalculateDistanceBetweenPoints(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            return Math.Sqrt(dx * dx + dy * dy);
        }

    }
}

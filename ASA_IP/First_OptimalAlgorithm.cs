using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    public static class First_OptimalAlgorithm
    {

        public static List<Location> TSP_Implement(double[,] adjMatrix, int start, List<Location> locations)
        {
            int V = locations.Count;
            var cities = new List<int>();
            for (int i = 0; i < V; i++)
            {
                if (i != start)
                {
                    cities.Add(i);
                }
            }

            double minDistance = double.MaxValue;
            List<Location> shortestPath = null;

            do
            {
                double currDistance = 0;
                int k = start;

                var path = new List<Location> { locations[start] };

                for (int i = 0; i < cities.Count; i++)
                {
                    currDistance += adjMatrix[k, cities[i]];
                    k = cities[i];
                    path.Add(locations[cities[i]]);
                }

                currDistance += adjMatrix[k, start];
                path.Add(locations[start]); // Add the starting location to the end of the path

                if (currDistance < minDistance)
                {
                    minDistance = currDistance;
                    shortestPath = path;
                }
            } while (NextPermutation(cities));

            return shortestPath;
        }

        private static bool NextPermutation(List<int> cities)
        {
            int i = cities.Count - 2;
            while (i >= 0 && cities[i] >= cities[i + 1])
            {
                i--;
            }

            if (i < 0)
            {
                return false;
            }

            int j = cities.Count - 1;
            while (cities[j] <= cities[i])
            {
                j--;
            }

            Swap(cities, i, j);
            Reverse(cities, i + 1, cities.Count - 1);
            return true;
        }
        private static void Swap(List<int> cities, int i, int j)
        {
            (cities[j], cities[i]) = (cities[i], cities[j]);
        }

        private static void Reverse(List<int> cities, int start, int end)
        {
            while (start < end)
            {
                Swap(cities, start, end);
                start++;
                end--;
            }
        }


    }
}

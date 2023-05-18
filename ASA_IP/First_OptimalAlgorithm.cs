using System.Collections.Generic;
using System.Linq;

namespace ASA_IP
{
    public static class First_OptimalAlgorithm
    {
        public static List<Location> First_Optimal(double[,] adjMatrix, int start, List<Location> locations)
        {
            int V = locations.Count; // c | 1
            var cities = Enumerable.Range(0, V).Where(i => i != start).ToList(); // c | n-1
            double minDistance = double.MaxValue; // c | 1
            List<Location> shortestPath = null; // c | 1

            do // c | (n-1)!
            {
                double currDistance = 0; // c | (n-1)!
                int k = start; // c | (n-1)!
                var path = new List<Location> { locations[start] }; // c | (n-1)!

                foreach (int i in cities) // c | (n-1)
                {
                    currDistance += adjMatrix[k, i]; // c | (n-1)
                    k = i; // c | (n-1)
                    path.Add(locations[i]); // c | (n-1)
                }

                currDistance += adjMatrix[k, start]; // c | (n-1)!
                path.Add(locations[start]); // c | (n-1)!

                if (currDistance < minDistance) // c | (n-1)!
                {
                    minDistance = currDistance; // c | (n-1)!
                    shortestPath = path; // c | (n-1)!
                }
            } while (NextPermutation(cities)); // c | (n-1)!

            return shortestPath; // c | 1
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

using System.Collections.Generic;
using System.Linq;

namespace ASA_IP
{
    public static class First_OptimalAlgorithm
    {

        public static List<Location> First_Optimal(double[,] adjMatrix, int start, List<Location> locations)
        {
            int V = locations.Count;
            var cities = Enumerable.Range(0, V).Where(i => i != start).ToList();
            double minDistance = double.MaxValue;
            List<Location> shortestPath = null;

            do
            {
                double currDistance = 0;
                int k = start;
                var path = new List<Location> { locations[start] };

                foreach (int i in cities)
                {
                    currDistance += adjMatrix[k, i];
                    k = i;
                    path.Add(locations[i]);
                }

                currDistance += adjMatrix[k, start];
                path.Add(locations[start]);

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

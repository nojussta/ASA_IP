using System.Collections.Generic;

namespace ASA_IP
{
    public class Second_LocalAlgorithm
    {
        public static List<Location> Second_Local(double[,] adjMatrix, int start, List<Location> locations)
        {
            int n = locations.Count;                                   // c | 1
            bool[] visited = new bool[n];                               // c | n
            List<Location> path = new List<Location> { locations[start] };   // c | 1
            visited[start] = true;                                     // c | 1

            while (path.Count < n)                                     // c | n
            {
                int lastCityIndex = path.Count - 1;                     // c | n-1
                double minDistance = double.MaxValue;                   // c | n-1
                int closestCityIndex = -1;                              // c | n-1

                for (int i = 0; i < n; i++)                             // c | n*(n-1)
                {
                    if (!visited[i] && adjMatrix[lastCityIndex, i] < minDistance)    // c | n*(n-1)
                    {
                        minDistance = adjMatrix[lastCityIndex, i];      // c | n*(n-1)
                        closestCityIndex = i;                           // c | n*(n-1)
                    }
                }

                if (closestCityIndex != -1)                             // c | n-1
                {
                    path.Add(locations[closestCityIndex]);               // c | n-1
                    visited[closestCityIndex] = true;                    // c | n-1
                }
            }

            path.Add(locations[start]);                                 // c | 1
            return path;                                                // c | 1
        }

    }
}

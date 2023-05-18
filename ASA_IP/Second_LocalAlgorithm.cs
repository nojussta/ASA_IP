using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    public class Second_LocalAlgorithm
    {
        public static List<Location> GreedyTSP(double[,] adjMatrix, int start, List<Location> locations, List<Location> original)
        {
            int n = locations.Count;
            bool[] visited = new bool[n];
            List<Location> path = new List<Location>();
            path.Add(locations[start]);
            visited[start] = true;

            while (path.Count < n)
            {
                int lastCityIndex = path.Count - 1;
                double minDistance = double.MaxValue;
                int closestCityIndex = -1;

                for (int i = 0; i < n; i++)
                {
                    if (!visited[i] && adjMatrix[lastCityIndex, i] < minDistance)
                    {
                        minDistance = adjMatrix[lastCityIndex, i];
                        closestCityIndex = i;
                    }
                }

                if (closestCityIndex != -1)
                {
                    path.Add(locations[closestCityIndex]);
                    visited[closestCityIndex] = true;
                }
            }

            path.Add(locations[start]);
            return path;
        }
    }
}

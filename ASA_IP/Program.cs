using ScottPlot.Palettes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ASA_IP
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            var data = "Duomenys.csv";
            var locations = InOutUtils.ReadLocationsFromFile(data);
            double[,] distances = FS_FormGraph.CalculateDistances(locations);
            int n = locations.Count;
            List<Location> set1 = new List<Location>();
            List<Location> set2 = new List<Location>();
            List<Location> set3 = new List<Location>();
            for (int i = 0; i < n; i++)
            {
                if (i < n / 3) { set1.Add(locations[i]); }
                else if (i >= n / 3 && i < 2 * n / 3) { set2.Add(locations[i]); }
                else { set3.Add(locations[i]); }
            }

            {//===========================================================================PIRMA UŽDUOTIS===========================================================================
                Console.WriteLine("--Pirmas--");
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                List<Location> sp1 = First_OptimalAlgorithm.TSP_Implement(distances, 0, set1);
                List<Location> sp2 = First_OptimalAlgorithm.TSP_Implement(distances, 0, set2);
                List<Location> sp3 = First_OptimalAlgorithm.TSP_Implement(distances, 0, set3);

                stopwatch.Stop();
                Application.Run(new FS_FormGraph(sp1));
                Application.Run(new FS_FormGraph(sp2));
                Application.Run(new FS_FormGraph(sp3));

                Console.WriteLine("Elapsed Time: {0} ms", stopwatch.Elapsed);
            }//===========================================================================PIRMA UŽDUOTIS===========================================================================

            {//===========================================================================TREČIA UŽDUOTIS===========================================================================
                // Nuskaitome duomenis iš failo ir sudedame juos į LocationContainer objektą
                LocationContainer Locations = InOutUtils.ReadData(data);

                // Konfigūruojame genetinio algoritmo parametrus
                int populationSize = 100;
                int maxGenerations = 1000;

                // Paleidžiame genetinį algoritmą ir gauname rezultatą - kelionės maršrutus

                Stopwatch sw = new Stopwatch();
                sw.Start();
                // Pirmas keliautojas
                LocationContainer firstLocations = new LocationContainer();
                List<Location> allLocations = Locations.GetAllLocations();
                for (int i = 0; i < allLocations.Count / 2; i++)
                {
                    firstLocations.AddLocation(allLocations[i]);
                }
                List<Route> firstRoutes = Third_GenethicAlgorithm.FindShortestRoutes(firstLocations, populationSize, maxGenerations);

                //Antras keliautojas
                LocationContainer secondLocations = new LocationContainer();
                for (int i = (allLocations.Count / 2) + 1; i < allLocations.Count; i++)
                {
                    secondLocations.AddLocation(allLocations[i]);
                }
                List<Route> secondRoutes = Third_GenethicAlgorithm.FindShortestRoutes(secondLocations, populationSize, maxGenerations);

                // Trečias keliautojas
                LocationContainer thirdLocations = new LocationContainer();
                for (int i = 2 * (allLocations.Count / 3); i < allLocations.Count; i++)
                {
                    thirdLocations.AddLocation(allLocations[i]);
                }
                List<Route> thirdRoutes = Third_GenethicAlgorithm.FindShortestRoutes(thirdLocations, populationSize, maxGenerations);
                sw.Stop();

                // Spausdiname geriausią rastą kelionės maršrutą pirmajam keliautojui
                Console.WriteLine("Pirmasis keliautojas:");
                Console.WriteLine("");
                Route firstShortestRoute = firstRoutes[0];
                firstShortestRoute.AddLocation(firstShortestRoute.VisitedLocations[0]);
                Console.WriteLine("Geriausias maršrutas:");
                foreach (Location Location in firstShortestRoute.VisitedLocations)
                {
                    Console.WriteLine("{0}. {1} ({2},{3})", Location.Id, Location.Name, Location.X, Location.Y);
                }
                Console.WriteLine("Viso atstumas: {0}", firstShortestRoute.GetTotalDistance());

                Console.WriteLine("\n");

                // Spausdiname geriausią rastą kelionės maršrutą antrajam keliautojui
                Console.WriteLine("Antrasis keliautojas:");
                Console.WriteLine("");
                Route secondShortestRoute = secondRoutes[0];
                secondShortestRoute.AddLocation(secondShortestRoute.VisitedLocations[0]);
                Console.WriteLine("Geriausias maršrutas:");
                foreach (Location Location in secondShortestRoute.VisitedLocations)
                {
                    Console.WriteLine("{0}. {1} ({2},{3})", Location.Id, Location.Name, Location.X, Location.Y);
                }
                Console.WriteLine("Viso atstumas: {0}", secondShortestRoute.GetTotalDistance());

                Console.WriteLine("\n");

                // Spausdiname geriausią rastą kelionės maršrutą trečiajam keliautojui
                Console.WriteLine("Trečiasis keliautojas:");
                Console.WriteLine("");
                Route thirdShortestRoute = thirdRoutes[0];
                thirdShortestRoute.AddLocation(thirdShortestRoute.VisitedLocations[0]);
                Console.WriteLine("Geriausias maršrutas:");
                foreach (Location Location in thirdShortestRoute.VisitedLocations)
                {
                    Console.WriteLine("{0}. {1} ({2},{3})", Location.Id, Location.Name, Location.X, Location.Y);
                }
                Console.WriteLine("Viso atstumas: {0}", thirdShortestRoute.GetTotalDistance());

                Console.WriteLine("");
                Console.WriteLine("Laikas per kurį apdoroti duomenys:");
                Console.WriteLine(sw.Elapsed);

                FormGraph.DrawGraph(firstShortestRoute, secondShortestRoute, thirdShortestRoute);
            }//===========================================================================TREČIA UŽDUOTIS===========================================================================
        }
    }
}

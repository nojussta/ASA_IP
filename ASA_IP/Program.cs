using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ASA_IP
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {

            // Nuskaitome duomenis iš failo ir sudedame juos į LocationContainer objektą
            LocationContainer Locations = InOutUtils.ReadData("Duomenys.csv");

            //===========================================================================TREČIA UŽDUOTIS===========================================================================
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
            //===========================================================================TREČIA UŽDUOTIS===========================================================================
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ScottPlot;
using System.Drawing;

namespace ASA_IP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Nuskaitome duomenis iš failo ir sudedame juos į LocationContainer objektą
            LocationContainer Locations = ReadLocationsFromFile("Duomenys.csv");

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
            List<Route> firstRoutes = GenethicAlgorithm.FindShortestRoutes(firstLocations, populationSize, maxGenerations);

            //Antras keliautojas
            LocationContainer secondLocations = new LocationContainer();
            for (int i = (allLocations.Count / 2) + 1; i < allLocations.Count; i++)
            {
                secondLocations.AddLocation(allLocations[i]);
            }
            List<Route> secondRoutes = GenethicAlgorithm.FindShortestRoutes(secondLocations, populationSize, maxGenerations);

            // Trečias keliautojas
            LocationContainer thirdLocations = new LocationContainer();
            for (int i = 2 * (allLocations.Count / 3); i < allLocations.Count; i++)
            {
                thirdLocations.AddLocation(allLocations[i]);
            }
            List<Route> thirdRoutes = GenethicAlgorithm.FindShortestRoutes(thirdLocations, populationSize, maxGenerations);
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

            // Spausdiname geriausią rastą kelionės maršrutą pirmajam keliautojui
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
            Console.WriteLine("Bendras užtruktas laikas:");
            Console.WriteLine(sw.Elapsed);

            DrawGraph(firstShortestRoute, secondShortestRoute, thirdShortestRoute);
        }

        public static void DrawGraph(Route firstRoute, Route secondRoute, Route thirdRoute)
        {
            int n1 = firstRoute.VisitedLocations.Count;
            int n2 = secondRoute.VisitedLocations.Count;
            int n3 = thirdRoute.VisitedLocations.Count;
            double[] xs = new double[n1 + n2 + n3];
            double[] ys = new double[n1 + n2 + n3];
            var plt = new Plot();

            for (int i = 0; i < n1; i++)
            {
                xs[i] = Math.Round(firstRoute.VisitedLocations[i].X / 2000);
                ys[i] = Math.Round(firstRoute.VisitedLocations[i].Y / 2000);
            }
            for (int i = 0; i < n2; i++)
            {
                xs[n1 + i] = Math.Round(secondRoute.VisitedLocations[i].X / 2000);
                ys[n1 + i] = Math.Round(secondRoute.VisitedLocations[i].Y / 2000);
            }
            for (int i = 0; i < n3; i++)
            {
                xs[n1 + n2 + i] = Math.Round(thirdRoute.VisitedLocations[i].X / 2000);
                ys[n1 + n2 + i] = Math.Round(thirdRoute.VisitedLocations[i].Y / 2000);
            }

            for (int i = 0; i < n1; i++)
            {
                plt.PlotLine(xs[i], ys[i], xs[i + 1], ys[i + 1], Color.Red, lineWidth: 0.5);
            }

            for (int i = n1; i < n1 + n2 - 1; i++)
            {
                plt.PlotLine(xs[i], ys[i], xs[i + 1], ys[i + 1], Color.Green, lineWidth: 0.5);
            }

            for (int i = n1 + n2; i < n1 + n2 + n3 - 1; i++)
            {
                plt.PlotLine(xs[i], ys[i], xs[i + 1], ys[i + 1], Color.Blue, lineWidth: 0.5);
            }

            plt.Grid(lineStyle: LineStyle.Dot);
            plt.Title("Genetinis optimizavimas");
            plt.AxisAuto(0.1, 0.1);
            plt.Frame(false);

            // Save the plot as an image file
            plt.SaveFig("grafas_genetinis.png", 1200, 800, false, 5);
        }


        public static LocationContainer ReadLocationsFromFile(string fileName)
        {
            LocationContainer allLocations = new LocationContainer();

            string[] Lines = File.ReadAllLines(fileName, Encoding.UTF8);

            for (int i = 0; i < Lines.Length; i++)
            {
                string[] Values = Lines[i].Split(new string[] { ";" }, StringSplitOptions.None);
                string name = Values[0];
                long id = long.Parse(Values[1]);
                double x = double.Parse(Values[2]);
                double y = double.Parse(Values[3]);
                allLocations.AddLocation(id, name, x, y);
            }
            return allLocations;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    internal static class Third_GenethicAlgorithm
    {
        private static readonly Random random = new Random();

        public static List<Route> FindShortestRoutes(LocationContainer LocationContainer, int populationSize, int maxGenerations)
        {
            // Inicijuojame pradinę populiaciją
            List<Route> population = CreatePopulation(LocationContainer, populationSize);

            // Vykdomos pagrindinės genetinio algoritmo iteracijos
            for (int generation = 1; generation <= maxGenerations; generation++)
            {
                EvaluatePopulation(population);

                // Ruošiamas populiacijos naujas kartas (sudarome naują populiaciją naudodami 
                // perskirstymą, kryžminimą, mutaciją ir selekciją)
                population = CreateNewPopulation(population, populationSize);
            }

            // Atliekame galutininę populiacijos vertinimą ir grąžiname kandidatus su mažiausia kelionės kaina
            EvaluatePopulation(population);
            return population.OrderBy(r => r.GetTotalDistance()).ToList();
        }

        private static List<Route> CreatePopulation(LocationContainer LocationContainer, int populationSize)
        {
            List<Route> population = new List<Route>();
            List<Location> tempLocations = LocationContainer.GetAllLocations();

            // Sukuriame pradinę populiaciją
            for (int i = 0; i < populationSize; i++)
            {
                Route route = new Route();

                // Sukuriame naują maršrutą pridėdami vietas iš kolekcijos atsitiktine tvarka
                List<Location> allLocations = new List<Location>();

                for (int j = 0; j < tempLocations.Count; j++)
                {
                    allLocations.Add(tempLocations[j]);
                }
                while (allLocations.Count > 0)
                {
                    int index = random.Next(0, allLocations.Count);
                    Location Location = allLocations[index];
                    allLocations.RemoveAt(index);

                    route.AddLocation(Location);
                }

                // Užbaigiame maršrutą pridedant pirmą vietą
                Location firstLocation = route.VisitedLocations.FirstOrDefault();
                route.AddLocation(firstLocation);

                population.Add(route);
            }

            return population;
        }

        private static void EvaluatePopulation(List<Route> population)
        {
            foreach (Route route in population)
            {
                double totalDistance = route.GetTotalDistance();

                // Nustatome atitinkamą kandidato kokybės vertę
                route.Quality = 1 / totalDistance;
            }
        }

        private static List<Route> CreateNewPopulation(List<Route> population, int tournamentSize)
        {
            List<Route> newPopulation = new List<Route>();

            for (int i = 0; i < tournamentSize / 2; i++)
            {
                Route parent1 = TournamentSelection(population, tournamentSize);
                Route parent2 = TournamentSelection(population, tournamentSize);

                Route children = Crossover(parent1, parent2);

                Mutate(children);

                newPopulation.Add(children);
            }

            return newPopulation;
        }

        private static Route TournamentSelection(List<Route> population, int tournamentSize)
        {
            List<Route> tournamentParticipants = new List<Route>();

            // Randomly select individuals for the tournament
            for (int i = 0; i < tournamentSize; i++)
            {
                int randomIndex = random.Next(population.Count);
                tournamentParticipants.Add(population[randomIndex]);
            }

            // Find the individual with the highest fitness (lowest distance)
            Route bestIndividual = tournamentParticipants.OrderBy(r => r.GetTotalDistance()).First();
            return bestIndividual;
        }

        // Funkcija atlieka kryžminimą (crossover) tarp dviejų maršrutų
        private static Route Crossover(Route parent1, Route parent2)
        {
            int point = 1;
            if (parent1.VisitedLocations.Count - 1 > 1) point = parent1.VisitedLocations.Count - 1;
            int crossoverPoint = new Random().Next(1, point);
            Route child = new Route
            {
                VisitedLocations = parent1.VisitedLocations.Take(crossoverPoint).ToList()
            };
            for (int i = 0; i < parent2.VisitedLocations.Count; i++)
            {
                Location currentGene = parent2.VisitedLocations[i];
                if (!child.ContainsLocation(currentGene))
                {
                    child.AddLocation(currentGene);
                }
            }
            return child;
        }

        private static void Mutate(Route route)
        {
            double mutationRate = 0.01;
            Random random = new Random();

            for (int i = 1; i < route.VisitedLocations.Count - 1; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    int j = random.Next(1, route.VisitedLocations.Count - 1);
                    InvertSegment(route, i, j);
                }
            }
        }

        private static void InvertSegment(Route route, int startIndex, int endIndex)
        {
            while (startIndex < endIndex)
            {
                (route.VisitedLocations[endIndex], route.VisitedLocations[startIndex]) = (route.VisitedLocations[startIndex], route.VisitedLocations[endIndex]);
                startIndex++;
                endIndex--;
            }
        }

    }
}

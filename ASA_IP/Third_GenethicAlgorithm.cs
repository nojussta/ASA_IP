using System;
using System.Collections.Generic;
using System.Linq;

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
            {
                EvaluatePopulation(population);   // c | populationSize

                population = CreateNewPopulation(population, populationSize);   // c | populationSize
                // perskirstymą, kryžminimą, mutaciją ir selekciją)
            }

            // Atliekame galutininę populiacijos vertinimą ir grąžiname kandidatus su mažiausia kelionės kaina
            return population.OrderBy(r => r.GetTotalDistance()).ToList();   // c | populationSize * log(populationSize)
        }

        private static List<Route> CreatePopulation(LocationContainer LocationContainer, int populationSize)
        {
            List<Route> population = new List<Route>();   // c | 1
            List<Location> tempLocations = LocationContainer.GetAllLocations();   // c | 1

            // Sukuriame pradinę populiaciją
            {
                Route route = new Route();   // c | 1

                // Sukuriame naują maršrutą pridėdami vietas iš kolekcijos atsitiktine tvarka

                while (allLocations.Count > 0)   // c | populationSize
                {
                    int index = random.Next(0, allLocations.Count);   // c | populationSize
                    Location Location = allLocations[index];   // c | populationSize
                    allLocations.RemoveAt(index);   // c | populationSize

                    route.AddLocation(Location);   // c | populationSize
                }

                // Užbaigiame maršrutą pridedant pirmą vietą
                route.AddLocation(firstLocation);   // c | 1

                population.Add(route);   // c | populationSize
            }

            return population;   // c | 1
        }

        private static void EvaluatePopulation(List<Route> population)
        {
            foreach (Route route in population)   // c | populationSize
            {
                double totalDistance = route.GetTotalDistance();   // c | 1

                // Nustatome atitinkamą kandidato kokybės vertę
            }
        }

        private static List<Route> CreateNewPopulation(List<Route> population, int tournamentSize)
        {
            List<Route> newPopulation = new List<Route>();   // c | 1

            for (int i = 0; i < tournamentSize / 2; i++)   // c | tournamentSize
            {
                Route parent1 = TournamentSelection(population, tournamentSize);   // c | tournamentSize
                Route parent2 = TournamentSelection(population, tournamentSize);   // c | tournamentSize

                Route children = Crossover(parent1, parent2);   // c | 1

                Mutate(children);   // c | 1

                newPopulation.Add(children);   // c | 1
            }

            return newPopulation;   // c | 1
        }

        private static Route TournamentSelection(List<Route> population, int tournamentSize)
        {
            List<Route> tournamentParticipants = new List<Route>();   // c | 1

            // Randomly select individuals for the tournament
            {
                int randomIndex = random.Next(population.Count);   // c | tournamentSize
                tournamentParticipants.Add(population[randomIndex]);   // c | tournamentSize
            }

            // Find the individual with the highest fitness (lowest distance)
            return bestIndividual;   // c | 1
        }

        // Funkcija atlieka kryžminimą (crossover) tarp dviejų maršrutų
        private static Route Crossover(Route parent1, Route parent2)
        {
            int point = 1;   // c | 1
            if (parent1.VisitedLocations.Count - 1 > 1) point = parent1.VisitedLocations.Count - 1;   // c | 1
            int crossoverPoint = new Random().Next(1, point);   // c | 1
            Route child = new Route   // c | 1
            {
                VisitedLocations = parent1.VisitedLocations.Take(crossoverPoint).ToList()   // c | crossoverPoint
            };
            for (int i = 0; i < parent2.VisitedLocations.Count; i++)   // c | parent2.VisitedLocations.Count
            {
                Location currentGene = parent2.VisitedLocations[i];   // c | parent2.VisitedLocations.Count
                if (!child.ContainsLocation(currentGene))   // c | parent2.VisitedLocations.Count
                {
                    child.AddLocation(currentGene);   // c | parent2.VisitedLocations.Count
                }
            }
            return child;   // c | 1
        }

        private static void Mutate(Route route)
        {
            double mutationRate = 0.01;   // c | 1
            Random random = new Random();   // c | 1

            for (int i = 1; i < route.VisitedLocations.Count - 1; i++)   // c | route.VisitedLocations.Count
            {
                if (random.NextDouble() < mutationRate)   // c | route.VisitedLocations.Count
                {
                    int j = random.Next(1, route.VisitedLocations.Count - 1);   // c | route.VisitedLocations.Count
                    InvertSegment(route, i, j);   // c | 1
                }
            }
        }

        private static void InvertSegment(Route route, int startIndex, int endIndex)
        {
            while (startIndex < endIndex)   // c | (endIndex - startIndex)
            {
                (route.VisitedLocations[endIndex], route.VisitedLocations[startIndex]) = (route.VisitedLocations[startIndex], route.VisitedLocations[endIndex]);   // c | (endIndex - startIndex)
                startIndex++;   // c | (endIndex - startIndex)
                endIndex--;   // c | (endIndex - startIndex)
            }
        }
    }

}

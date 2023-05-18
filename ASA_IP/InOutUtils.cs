using System.IO;
using System.Collections.Generic;

namespace ASA_IP
{
    internal class InOutUtils
    {
        public static LocationContainer ReadData(string fileName)
        {
            LocationContainer locations = new LocationContainer();

            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] Values = line.Split(';');

                string name = Values[0];
                long id = long.Parse(Values[1]);
                double X = double.Parse(Values[2]);
                double Y = double.Parse(Values[3]);
                locations.AddLocation(id, name, X, Y);
            }

            return locations;
        }

        public static List<Location> ReadLocationsFromFile(string path)
        {
            var locations = new List<Location>();

            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var location = Location.CreateLocation(line);
                    locations.Add(location);
                }
            }

            return locations;
        }
    }

}

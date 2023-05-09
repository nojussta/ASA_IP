using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    internal class InOutUtils
    {
        public static List<Location> ReadData(string fileName)
        {
            List<Location> locations = new List<Location>();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] Values = line.Split('\t');

                string name = Values[0];
                long id = long.Parse(Values[1]);
                double X = double.Parse(Values[2]);
                double Y = double.Parse(Values[3]);

                Location location = new Location(name, id, X, Y);
                locations.Add(location);
            }
            return locations;
        }
    }
}

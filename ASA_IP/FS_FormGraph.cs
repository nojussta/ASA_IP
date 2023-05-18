using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ASA_IP
{
    internal class FS_FormGraph : Form
    {
        private readonly List<Location> path;
        private readonly double minX;
        private readonly double maxX;
        private readonly double minY;
        private readonly double maxY;
        private new const int Padding = 50;

        public FS_FormGraph(List<Location> path)
        {
            this.path = path;

            // Find the minimum and maximum values of X and Y coordinates
            minX = maxX = path[0].X;
            minY = maxY = path[0].Y;

            foreach (Location location in path)
            {
                minX = Math.Min(minX, location.X);
                maxX = Math.Max(maxX, location.X);
                minY = Math.Min(minY, location.Y);
                maxY = Math.Max(maxY, location.Y);
            }

            // Subscribe to the Paint event to draw the graph
            Paint += FS_FormGraph_Paint;
        }

        private void FS_FormGraph_Paint(object sender, PaintEventArgs e)
        {
            // Create a Graphics object from the PaintEventArgs
            Graphics g = e.Graphics;

            // Calculate the scaling factors
            double xScale = (ClientSize.Width - 2 * Padding) / (maxX - minX);
            double yScale = (ClientSize.Height - 2 * Padding) / (maxY - minY);

            // Set up the pen for drawing the path
            using (Pen pen = new Pen(Color.Red, 2))
            {
                // Draw the path
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Location start = path[i];
                    Location end = path[i + 1];

                    // Scale the coordinates
                    int startX = (int)((start.X - minX) * xScale) + Padding;
                    int startY = (int)((start.Y - minY) * yScale) + Padding;
                    int endX = (int)((end.X - minX) * xScale) + Padding;
                    int endY = (int)((end.Y - minY) * yScale) + Padding;

                    // Draw the line
                    g.DrawLine(pen, startX, startY, endX, endY);

                    // Calculate the midpoint for placing the length and name
                    int midX = (startX + endX) / 2;
                    int midY = (startY + endY) / 2;

                    // Calculate the length of the edge
                    double length = FindDistance(start, end);

                    // Draw the length
                    string lengthText = length.ToString("0.##");
                    g.DrawString(lengthText, Font, Brushes.Black, midX, midY);

                    // Draw the name of each point
                    g.DrawString(start.Name, Font, Brushes.Black, startX, startY);
                    g.DrawString(end.Name, Font, Brushes.Black, endX, endY);
                }
            }
        }

        public static double FindDistance(Location l1, Location l2)
        {
            return Math.Sqrt(Math.Pow(l2.X - l1.X, 2) + Math.Pow(l2.Y - l1.Y, 2));
        }

        static internal double[,] CalculateDistances(List<Location> places)
        {
            var distances = new double[places.Count, places.Count];

            for (int i = 0; i < places.Count; i++)
            {
                for (int j = i + 1; j < places.Count; j++)
                {
                    var distance = FindDistance(places[i], places[j]);
                    distances[i, j] = distance;
                    distances[j, i] = distance;
                }
            }

            return distances;
        }
    }
}

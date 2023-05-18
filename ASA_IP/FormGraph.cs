using ScottPlot;
using System;
using System.Drawing;


namespace ASA_IP
{
    static internal class FormGraph
    {
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

            plt.SaveFig("Genetinis.png", 1200, 800, false, 5);
        }
    }
}

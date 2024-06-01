using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCalc
{
    public partial class MainWindow : Window
    {
        private void DrawingGraph(List<double> coordinates_x, List<double> coordinates_y, int zoom)
        {
            LineSeries functionGraph = new LineSeries
            {
                StrokeThickness = 2,
                Color = OxyColors.Black
            };

            for (int i = 0; i < coordinates_x.Count; i++)
            {
                functionGraph.Points.Add(new DataPoint(coordinates_x[i] * zoom, coordinates_y[i] * zoom));
            }

            PlotModel plotModel = new PlotModel();
            plotModel.Series.Add(functionGraph);
            plotModel.PlotAreaBorderThickness = new OxyThickness(0.5);
            plotModel.PlotAreaBorderColor = OxyColors.BlueViolet;
            plotModel.PlotMargins = new OxyThickness(1);

            LinearAxis linearAxis1 = new LinearAxis
            {
                Title = "y",
                TitlePosition = 0.05,
                AxisTitleDistance = 15,
                Maximum = 10,
                Minimum = -10,
                PositionAtZeroCrossing = true,
            };

            plotModel.Axes.Add(linearAxis1);

            LinearAxis linearAxis2 = new LinearAxis
            {
                Title = "x",
                TitlePosition = 0.01,
                AxisTitleDistance = 15,
                Maximum = 15,
                Minimum = -15,
                PositionAtZeroCrossing = true,
                Position = AxisPosition.Bottom,
            };

            plotModel.Axes.Add(linearAxis2);
            plotView.Model = plotModel;

        }
    }
}

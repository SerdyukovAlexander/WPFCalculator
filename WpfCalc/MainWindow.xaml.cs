using System;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot.Axes;
using OxyPlot.Wpf;


namespace WpfCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        
        private void DrawGraphic(List<double> coordinates_x, List<double> coordinates_y, int m)
        {
            var lineSeries = new LineSeries
            {
                StrokeThickness = 2,
                Color = OxyColors.Black
            };

            for (int i = 0; i < coordinates_x.Count; i++)
            {
                lineSeries.Points.Add(new DataPoint(coordinates_x[i]*m, coordinates_y[i]*m));
            }

            var plotModel = new PlotModel();
            plotModel.Series.Add(lineSeries);
            plotModel.PlotAreaBorderThickness = new OxyThickness(0.5);
            plotModel.PlotAreaBorderColor = OxyColors.BlueViolet;
            plotModel.PlotMargins = new OxyThickness(1);

            LinearAxis linearAxis = new LinearAxis
            {
                Title = "y",
                TitlePosition = 0.05,
                AxisTitleDistance = 15,
                Maximum = 10,
                Minimum = -10,
                PositionAtZeroCrossing = true,
            };

            plotModel.Axes.Add(linearAxis);

            LinearAxis secondLinearAxis = new LinearAxis
            {
                Title = "x",
                TitlePosition = 0.01,
                AxisTitleDistance = 15,
                Maximum = 15,
                Minimum = -15,
                PositionAtZeroCrossing = true,
                Position = AxisPosition.Bottom,
            };

            plotModel.Axes.Add(secondLinearAxis);
            plotView.Model = plotModel;

        }

        private void Сalculating_Coordinates(string str_rpn, double beginSt, double endSt, double step_X, int scale_x)
        {
            List<double> coordinatesX = new List<double>();
            List<double> coordinatesY = new List<double>();
            double coordinateX = beginSt;

            while (coordinateX <= endSt)
            {
                double coordinateY = CalculationRPN.CalculateRPN(str_rpn, coordinateX);
                coordinateY = Math.Round(coordinateY, 3);

                if (!double.IsInfinity(coordinateY))
                {
                    coordinatesX.Add(coordinateX);
                    coordinatesY.Add(coordinateY);
                }

                coordinateX += step_X;
                coordinateX = Math.Round(coordinateX, 3);
            }

            DrawGraphic(coordinatesX, coordinatesY, scale_x);
        }


        public void Showresult_Click(object sender, RoutedEventArgs e)
        {
            string[] input = inputExpression.ToString().Split(' ');
            string expression1 = input[1];
            double beginstepX = Convert.ToDouble(beginX.Text);
            double endstepX = Convert.ToDouble(endX.Text);
            double step_x = Convert.ToDouble(stepX.Text);
            int scaleX = Convert.ToInt32(scale.Text);
            string rpn = ConvertingToRPN.InfixToRPN(expression1);

            Сalculating_Coordinates(rpn, beginstepX, endstepX, step_x, scaleX);
        }
    }
}

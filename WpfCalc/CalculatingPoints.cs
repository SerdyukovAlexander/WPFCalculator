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
        public void CalculatingPoints(string str_rpn, double beginSt, double endSt, double step_X, int scale_x)
        {
            List<double> coordinatesX = new List<double>();
            List<double> coordinatesY = new List<double>();
            double coordinateX = beginSt;

            while (coordinateX <= endSt)
            {
                double coordinateY = CalculationRPN(str_rpn, coordinateX);
                coordinateY = Math.Round(coordinateY, 3);

                if (!double.IsInfinity(coordinateY))
                {
                    coordinatesX.Add(coordinateX);
                    coordinatesY.Add(coordinateY);
                }

                coordinateX += step_X;
                coordinateX = Math.Round(coordinateX, 3);
            }

            DrawingGraph(coordinatesX, coordinatesY, scale_x);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
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
        
        private void Draw_Graphic(int[] coordinates_x, double[] coordinates_y, int m)
        {
            polyline.Points = new PointCollection();

            for (int i = 0; i < coordinates_x.Length; i++)
            {
                polyline.Points.Add(new Point((coordinates_x[i])*m, (coordinates_y[i])*m ));
            }

        }

        public void Showresult_Click(object sender, RoutedEventArgs e)
        {
            string input = inputExpression.ToString();
            int beginstepX = Convert.ToInt32(beginX.Text);
            int endstepX = Convert.ToInt32(endX.Text);
            int step_x = Convert.ToInt32(stepX.Text);
            int scaleX = Convert.ToInt32(scale.Text);
            string rpn = Infix_To_RPN.InfixToRPN(input);
            int countPoints = 0;
            for (int l = beginstepX; l < endstepX; l += step_x)
            {
                countPoints += 1;
            }

            int[] coordinatesX = new int[countPoints+1];
            double[] coordinatesY = new double[countPoints+1];
            int cnt = 0;
            for (int coordinateX = beginstepX; coordinateX <= endstepX; coordinateX += step_x)
            {
                double coordinateY = Calculate_RPN.CalculateRPN(rpn, coordinateX);
                coordinatesX[cnt] = coordinateX;
                coordinatesY[cnt] = coordinateY;
                cnt++;
            }

            Draw_Graphic(coordinatesX, coordinatesY, scaleX);
        }
    }
}

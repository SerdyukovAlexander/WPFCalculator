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
    
        public void ConvertingInputData(object sender, RoutedEventArgs e)
        {
            string[] input = inputExpression.ToString().Split(' ');
            string expression1 = input[1];
            double beginstepX = Convert.ToDouble(beginX.Text);
            double endstepX = Convert.ToDouble(endX.Text);
            double step_x = Convert.ToDouble(stepX.Text);
            int scaleX = Convert.ToInt32(scale.Text);
            string rpn = ConvertingToRPN(expression1);

            CalculatingPoints(rpn, beginstepX, endstepX, step_x, scaleX);
        }
    }
}

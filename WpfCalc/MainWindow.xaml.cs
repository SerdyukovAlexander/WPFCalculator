﻿using System;
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

        public void Showresult_Click(object sender, RoutedEventArgs e)
        {
            string input = inputExpression.ToString();
            double x = Convert.ToDouble(inputX.Text);

            string rpn = Infix_To_RPN.InfixToRPN(input);
            double result = Calculate_RPN.CalculateRPN(rpn, x);
            resultstr.Text = result.ToString();
        }
    }
}

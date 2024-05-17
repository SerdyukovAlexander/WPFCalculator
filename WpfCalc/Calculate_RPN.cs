using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalc
{
    public class Calculate_RPN
    {
        public static double CalculateRPN(string rpn)
        {
            string[] tokens = rpn.Split(' ');
            Stack<double> numberStack = new Stack<double>();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double number))
                {
                    numberStack.Push(number);
                }
                else if (numberStack.Count >= 2)
                {
                    double operand2 = numberStack.Pop();
                    double operand1 = numberStack.Pop();

                    switch (token)
                    {
                        case "+":
                            numberStack.Push(operand1 + operand2);
                            break;
                        case "-":
                            numberStack.Push(operand1 - operand2);
                            break;
                        case "*":
                            numberStack.Push(operand1 * operand2);
                            break;
                        case "/":
                            numberStack.Push(operand1 / operand2);
                            break;
                    }
                }
            }

            return numberStack.Count > 0 ? numberStack.Peek() : 0;
        }
    }
}

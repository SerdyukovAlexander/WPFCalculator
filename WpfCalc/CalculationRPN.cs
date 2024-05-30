using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalc
{
    public class CalculationRPN
    {
        public static double CalculateRPN(string rpn, double coordinate_X)
        {
            string[] tokens = rpn.Split(' ');
            Stack<double> numberStack = new Stack<double>();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double number))
                {
                    numberStack.Push(number);
                }
                else if (token == "")
                {
                    continue;
                }

                else if (token == "x")
                {
                    numberStack.Push(coordinate_X);
                }

                else if (token.Length > 3 && (token.Substring(0, 3) == "log" || token.Substring(0, 3) == "srt"))
                {
                    string tokenstr = Convert.ToString(token);
                    string a = "";
                    string b = "";
                    int cnt = 4;
                    while (tokenstr[cnt] != ',')
                    {
                        a += tokenstr[cnt];
                        cnt += 1;
                    }
                    cnt += 1;
                    while (cnt < tokenstr.Length - 1)
                    {
                        b += tokenstr[cnt];
                        cnt += 1;
                    }

                    a = ConvertingToRPN.InfixToRPN(a);
                    b = ConvertingToRPN.InfixToRPN(b);
                    double a1 = CalculateRPN(a, coordinate_X);
                    double b1 = CalculateRPN(b, coordinate_X);
                    if (token.Substring(0, 3) == "log")
                    {
                        numberStack.Push(Math.Log(b1, a1));
                    }
                    if (token.Substring(0, 3) == "srt")
                    {
                        numberStack.Push(Math.Pow(b1, 1 / a1));
                    }
                }
                else if (token.Length > 3 && (token.Substring(0, 3) == "sin" || token.Substring(0, 3) == "cos" || token.Substring(0, 3) == "tan" || token.Substring(0, 3) == "ctn" || token.Substring(0, 3) == "sqr"))
                {
                    string numInside = "";

                    for (int i = 4; i < token.Length - 1; i++)
                    {
                        if (double.TryParse(Convert.ToString(token[i]), out double num))
                        {
                            numInside += num;
                        }
                        else if (Convert.ToString(token[i]) == "x")
                        {
                            numInside += "x";
                        }
                        else
                        {
                            numInside += Convert.ToString(token[i]);
                        }
                    }

                    numInside = ConvertingToRPN.InfixToRPN(numInside);
                    double intoNumber = CalculateRPN(numInside, coordinate_X);

                    switch (Convert.ToString(token.Substring(0, 3)))
                    {
                        case "sin":
                            numberStack.Push(Math.Sin(intoNumber));
                            break;
                        case "cos":
                            numberStack.Push(Math.Cos(intoNumber));
                            break;
                        case "tan":
                            numberStack.Push(Math.Tan(intoNumber));
                            break;
                        case "ctn":
                            numberStack.Push(Math.Cos(intoNumber) / Math.Sin(intoNumber));
                            break;
                        case "sqr":
                            numberStack.Push(Math.Sqrt(intoNumber));
                            break;
                    }
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
                        case "^":
                            numberStack.Push(Math.Pow(operand1, operand2));
                            break;
                    }
                }
            }

            return numberStack.Count > 0 ? numberStack.Peek() : 0;
        }
    }
}

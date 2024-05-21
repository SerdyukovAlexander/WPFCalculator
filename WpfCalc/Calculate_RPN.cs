using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalc
{
    public class Calculate_RPN
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


                else if (token == "x")
                {
                    numberStack.Push(coordinate_X);
                }

                else if(token.Contains("srt") || token.Contains("log"))
                {
                    // log(a, b) srt(a, b)
                    string a = "";
                    string b = "";
                    int cnt = 4;
                    while (Convert.ToString(token[cnt]) != "," )
                    {
                        a += Convert.ToString(token[cnt]) + " ";
                        cnt += 1;
                    }
                    cnt += 1;
                    while (Convert.ToString(token[cnt]) != ")")
                    {
                        b += Convert.ToString(token[cnt]) + " ";
                        cnt += 1;
                    }

                    a = Infix_To_RPN.InfixToRPN(a);
                    b = Infix_To_RPN.InfixToRPN(b);
                    double a1 = CalculateRPN(a, coordinate_X);
                    double b1 = CalculateRPN(b, coordinate_X);

                    if (token.Substring(0, 3) == "srt")
                    {
                        numberStack.Push(Math.Pow(b1, 1/a1));
                    }
                    if (token.Substring(0, 3) == "log")
                    {
                        numberStack.Push(Math.Log(b1, a1));
                    }
                }

                else if ( token.Contains("sin") || token.Contains("cos") || token.Contains("tan") || token.Contains("ctn") || token.Contains("sqr"))
                {
                    string numInside = "";

                    for (int i = 4; i < token.Length-1; i++)
                    {
                        if ( double.TryParse(Convert.ToString(token[i]), out double num) )
                        {
                            numInside += num + " ";
                        }
                        else if (Convert.ToString(token[i]) == "x")
                        {
                            numInside += coordinate_X + " ";
                        }
                        else
                        {
                            numInside += Convert.ToString(token[i]) + " ";
                        }
                    }

                    numInside = Infix_To_RPN.InfixToRPN(numInside);
                    double intoNumber = CalculateRPN(numInside, coordinate_X);

                    switch ( Convert.ToString(token.Substring(0, 3)) )
                    {
                        case "sin":
                            numberStack.Push( Math.Sin(intoNumber) );
                            break;
                        case "cos":
                            numberStack.Push( Math.Cos(intoNumber) );
                            break;
                        case "tan":
                            numberStack.Push( Math.Tan(intoNumber) );
                            break;
                        case "ctn":
                            numberStack.Push( Math.Cos(intoNumber) / Math.Sin(intoNumber) );
                            break;
                        case "sqr":
                            numberStack.Push( Math.Sqrt(intoNumber) );
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
                //
            }

            return numberStack.Count > 0 ? numberStack.Peek() : 0;
        }
    }
}

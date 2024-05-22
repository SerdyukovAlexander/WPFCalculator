﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalc
{
    public class Infix_To_RPN
    {
        public static string InfixToRPN(string infix)
        {
            //log(10,sin(x^2)) или (х*4,5-6*(12-4)) => (x * 4,5 - 6 * ( 12 - 4 ))
            //метод для нормального разделения токенов
            string allNumbers = "0123456789,x";
            string allOperators = "+-*/^()";
            string[] tokens = new string[1];
            string expUpdate = "";
            if (!infix.Contains("sin") && !infix.Contains("cos") && !infix.Contains("tan") && !infix.Contains("ctn") && !infix.Contains("sqr") && !infix.Contains("srt") && !infix.Contains("log"))
            {
                for (int i = 0; i<infix.Length; i++)
                {
                    if (allNumbers.Contains(infix[i]))
                    {
                        expUpdate += infix[i];
                    }
                    else if (allOperators.Contains(infix[i]))
                    {
                        expUpdate += " " + infix[i] + " ";
                    }
                }
                tokens = expUpdate.Split(' ');
            }
            else
            {
                tokens[0] = infix;
            }

            string rpn = "";
            Stack<string> operatorStack = new Stack<string>();

            Dictionary<string, int> precedence = new Dictionary<string, int>
            {
                { "+", 1 },
                { "-", 1 },
                { "*", 2 },
                { "/", 2 },
                { "^", 3 },
            };

            foreach (string token in tokens)
            {

                if (double.TryParse(token, out double number))
                {
                    rpn += number + " ";
                }

                else if (precedence.ContainsKey(token))
                {
                    while (operatorStack.Count > 0 &&
                           precedence.ContainsKey(operatorStack.Peek()) &&
                           precedence[token] <= precedence[operatorStack.Peek()])
                    {
                        rpn += operatorStack.Pop() + " ";
                    }
                    operatorStack.Push(token);
                }

                else if (token == "(")
                {
                    operatorStack.Push(token);
                }

                else if (token == ")")
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        rpn += operatorStack.Pop() + " ";
                    }
                    if (operatorStack.Count > 0 && operatorStack.Peek() == "(")
                    {
                        operatorStack.Pop();
                    }
                }

                else if (token == "x")
                {
                    rpn += "x" + " ";
                }

                else if (token.Contains("sin") || token.Contains("cos") || token.Contains("tan") || token.Contains("ctn") || token.Contains("log") || token.Contains("sqr") || token.Contains("srt"))
                {
                    rpn += token + " ";
                }
            }

            while (operatorStack.Count > 0)
            {
                rpn += operatorStack.Pop() + " ";
            }

            return rpn;
        }
    }
}

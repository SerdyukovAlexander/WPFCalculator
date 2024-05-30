using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalc
{
    public class ConvertingToRPN
    {
        public static string InfixToRPN(string infix)
        {
            string allNumbers = "0123456789";
            string allOperators = "+-*/^";
            string wordInExpression = "sincotagrl";
            string[] tokens = new string[1];
            string expUpdate = "";
            
            for (int i = 0; i<infix.Length; i++)
            {
                if (allNumbers.Contains(infix[i]))
                {
                    expUpdate += infix[i];
                }
                else if (allOperators.Contains(infix[i]))
                {
                    expUpdate +=" " + infix[i] + " ";
                }
                else if (infix[i]=='x')
                {
                    expUpdate += "x";
                }
                else if (infix[i]=='(' || infix[i]==')')
                {
                    expUpdate += infix[i];
                }
                else if (wordInExpression.Contains(infix[i]))
                {
                    int cntOpenBrackets = 0;
                    int cntCloseBrackets = 0;
                    while (true)
                    {
                        if (infix[i]=='(')
                        {
                            cntOpenBrackets += 1;
                            expUpdate += infix[i];
                            i += 1;
                        }
                        else if (infix[i]==')')
                        {
                            cntCloseBrackets += 1;
                            expUpdate += infix[i];
                            if (cntOpenBrackets==cntCloseBrackets)
                                break;
                            i += 1;
                        }
                        else
                        {
                            expUpdate += infix[i];
                            i += 1;
                        }
                        
                    }
                }
            }
            tokens = expUpdate.Split(' ');

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

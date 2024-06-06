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
        public static string ProcessingInputString(string input)
        {
            string inputStr = input;
            string allNumbers = "0123456789";
            string allOperators = "+-*/^";
            string wordInExpression = "sincotagrl";
            string resultStr = "";

            for (int i = 0; i < inputStr.Length; i++)
            {
                if (allNumbers.Contains(inputStr[i]))
                {
                    resultStr += inputStr[i];
                }
                else if (allOperators.Contains(inputStr[i]))
                {
                    resultStr += " " + inputStr[i] + " ";
                }
                else if (inputStr[i] == 'x')
                {
                    resultStr += "x";
                }
                else if (inputStr[i] == '(' || inputStr[i] == ')')
                {
                    resultStr += inputStr[i];
                }
                else if (wordInExpression.Contains(inputStr[i]))
                {
                    int cntOpenBrackets = 0;
                    int cntCloseBrackets = 0;
                    while (true)
                    {
                        if (inputStr[i] == '(')
                        {
                            cntOpenBrackets += 1;
                            resultStr += inputStr[i];
                            i += 1;
                        }
                        else if (inputStr[i] == ')')
                        {
                            cntCloseBrackets += 1;
                            resultStr += inputStr[i];
                            if (cntOpenBrackets == cntCloseBrackets)
                                break;
                            i += 1;
                        }
                        else
                        {
                            resultStr += inputStr[i];
                            i += 1;
                        }

                    }
                }
            }

            return resultStr;
        }
        public static string ConvertingToRPN(string infix)
        {
            
            string[] tokens = new string[1];
            string expUpdate = ProcessingInputString(infix);
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

                else if (token.Length>3 && token.Substring(0,3)=="sin")
                {
                    rpn += token + " ";
                }
                else if (token.Length>3 && token.Substring(0,3)=="cos")
                {
                    rpn += token + " ";
                }
                else if (token.Length>3 && token.Substring(0,3)=="tan")
                {
                    rpn += token + " ";
                }
                else if (token.Length>3 && token.Substring(0,3)=="ctn")
                {
                    rpn += token + " ";
                }
                else if (token.Length>3 && token.Substring(0,3)=="log")
                {
                    rpn += token + " ";
                }
                else if (token.Length>3 && token.Substring(0,3)=="sqr")
                {
                    rpn += token + " ";
                }
                else if (token.Length>3 && token.Substring(0,3)=="srt")
                {
                    rpn+= token + " ";
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

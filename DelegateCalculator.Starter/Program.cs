using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCalculator
{
    class Program
    {
        delegate double MathOperation(double number1, double number2);

        static void Main(string[] args)
        {
            DisplayOpeningScreen();
            SetupOperations();
            ManageApplicationLoop();
            DisplayClosingScreen();
        }

        /// <summary>
        /// manage the application loop
        /// performing the math division with zero
        /// </summary>
        static void ManageApplicationLoop()
        {
            do
            {
                MathOperation operation;
                double operand1, operand2;
 

                Dictionary<MyMath.Operation, MathOperation> operationsDictionary = new Dictionary<MyMath.Operation, MathOperation>();

                operationsDictionary = SetupOperations();
                operation = DisplayGetOperation(operationsDictionary);
                operand1 = DisplayGetOperand(1);

                if (operation == MyMath.Divide)
                {
                    do
                    {
                        operand2 = DisplayGetOperand(2);
                    } while (!ValidateOperand(operand2));
                }
                else
                {
                    operand2 = DisplayGetOperand(2);
                }
                DisplayCalculation(operation, operand1, operand2);
            } while (DisplayExitPrompt());
        }
        /// <summary>
        /// Handle division by zero
        /// </summary>
        /// <param name="operand2"></param>
        /// <returns></returns>
        private static bool ValidateOperand(double operand2)
        {
            double zero = 0;
            if (operand2 == zero)
            {
                Console.WriteLine("Please pick a different number because you cannot divide by zero in this calculator");
                Console.ReadKey();
                return false;
            }
            else
            {
                return true;
            }
        }

            /// <summary>
            /// display all of the calculation information
            /// </summary>
            /// <param name="operation">delegate operation</param>
            /// <param name="operand1">operand 1</param>
            /// <param name="oprand2">operand 2</param>
            static void DisplayCalculation(MathOperation operation, double operand1, double oprand2)
        {
            double answer;
            answer = operation(operand1, oprand2);
            string methodName = operation.Method.Name;
            DisplayHeader("Calculation");

            Console.WriteLine($"Operation: {operation.Method.Name}");
            Console.WriteLine($"Operand 1: {operand1}");

            if (methodName != "Abs" && methodName != "Factorial")
            {
                Console.WriteLine($"Operand 2: {oprand2}");
            }
            
            Console.WriteLine($"Answer: {answer}");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// build out the dictionary of math operations that will be available to the application
        /// </summary>
        /// <returns>dictionary of operation enums and methods</returns>
        static Dictionary<MyMath.Operation, MathOperation> SetupOperations()
        {
            Dictionary<MyMath.Operation, MathOperation> operationsDictionary = new Dictionary<MyMath.Operation, MathOperation>
            {
                { MyMath.Operation.ADD, MyMath.Add },
                { MyMath.Operation.SUBTRACT, MyMath.Subtract },
                { MyMath.Operation.MULTIPLY, MyMath.Multiply },
                { MyMath.Operation.DIVIDE, MyMath.Divide },
                { MyMath.Operation.MODULO, MyMath.Modulo },
                { MyMath.Operation.EXPONENT, MyMath.Exponent },
                { MyMath.Operation.NTHROOT, MyMath.NthRoot },
                { MyMath.Operation.FACTORIAL, MyMath.Factorial },
                { MyMath.Operation.ABS, MyMath.Abs }


            };

            return operationsDictionary;
        }

        /// <summary>
        /// display opening screen
        /// </summary>
        static void DisplayOpeningScreen()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("\t\tWelcome to the Delegate Calculator App");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("\t\tThanks for using the Delegate Calculator App");
            Console.WriteLine();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display header
        /// </summary>
        static void DisplayHeader(string headerTitle)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerTitle);
            Console.WriteLine();
        }


        /// <summary>
        /// get a double operand from user
        /// adapted from code by Jacob Lakies
        /// </summary>
        /// <returns>double operand</returns>
        static double DisplayGetOperand(int operandNumber)
        {
            double zero = 0.0;
            double operand;

            do
            {
                DisplayHeader("Get Operand");
                Console.Write($"Enter Operand {operandNumber}:");
            } while (!ValidateDouble(Console.ReadLine(), out operand));

            return operand;
        }

        /// <summary>
        /// validate a string as a double
        /// </summary>
        /// <param name="userAnswer">user input value</param>
        /// <param name="operand">out variable if a valid double is input</param>
        /// <returns>result of validation</returns>
        static bool ValidateDouble(string userAnswer, out double operand)
        {
            bool validOperand = false;

            if (double.TryParse(userAnswer, out operand))
            {
                validOperand = true;
            }
            else 
            {
                Console.WriteLine();
                Console.WriteLine("Please try again and only enter a single number.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }

            return validOperand;
        }

        /// <summary>
        /// validate a string as an operation
        /// adapted from code by Jacob Lakies
        /// </summary>
        /// <param name="userAnswer">user input value</param>
        /// <param name="operand">out variable if a valid operation is input</param>
        /// <returns>result of validation</returns>
        static MathOperation DisplayGetOperation(Dictionary<MyMath.Operation, MathOperation> operationsDictionary)
        {
            MathOperation operation;

            do
            {
                DisplayHeader("Get Operation");
                Console.Write("Enter Operation:");
            } while (!ValidateOperation(Console.ReadLine(), operationsDictionary, out operation));

            return operation;
        }

        /// <summary>
        /// validate string as an operation in the operation dictionary
        /// </summary>
        /// <param name="userAnswer"></param>
        /// <param name="operationsDictionary"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        static bool ValidateOperation(string userAnswer, Dictionary<MyMath.Operation, MathOperation> operationsDictionary, out MathOperation operation)
        {
            bool validOperation = false;
            operation = null;
            MyMath.Operation operationChoice;

            //
            // parse user input for a valid operation enum
            //
            Enum.TryParse<MyMath.Operation>(userAnswer.ToUpper(), out operationChoice);

            //
            // if valid Operation enum AND in an operation in the dictionary set the return delegate
            //
            if (operationsDictionary.ContainsKey(operationChoice))
            {
                operation = operationsDictionary[operationChoice];
                validOperation = true;
            }
            else // provide user feedback for invalid response
            {
                Console.WriteLine();
                Console.WriteLine("Please try again and enter an operation from the following list.");
                Console.WriteLine();

                //
                // list all operations in the dictionary
                //
                Console.Write(" | ");
                foreach (KeyValuePair<MyMath.Operation, MathOperation> operationName in operationsDictionary)
                {
                    Console.Write(operationName.Key + " | ");
                }

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }

            return validOperation;
        }

        /// <summary>
        /// Exit screen with validation 
        /// </summary>
        /// <returns></returns>
        static bool DisplayExitPrompt()
        {
            DisplayHeader("Need to do more Math!");
            bool exit = true;
            bool validResponse = false;

            while (exit)
            {
                Console.Write("Do you wish to perform another calculation (yes/no)?");
                string userAnswer = Console.ReadLine().ToLower();

                switch (userAnswer)
                {
                    case "yes":
                        validResponse = true;
                        exit = false;
                        break;
                    case "y":
                        validResponse = true;
                        exit = false;
                        break;
                    case "no":
                        exit = false;
                        break;
                    case "n":
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("You did not enter a valid answer. A valid answer will yes or no. Please try again.");
                        break;
                }
            }

            return validResponse;
        }
    }
}


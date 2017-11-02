using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCalculator
{
    public static class MyMath
    {
        public enum Operation
        {
            NONE,
            ADD,
            SUBTRACT,
            MULTIPLY,
            DIVIDE,
            MODULO,
            EXPONENT,
            NTHROOT,
            FACTORIAL,
            ABS
        }

        public static double Add(double number1, double number2)
        {
            return number1 + number2;
        }

        public static double Subtract(double number1, double number2)
        {
            return number1 - number2;
        }

        public static double Multiply(double number1, double number2)
        {
            return number1 * number2;
        }

        public static double Divide(double number1, double number2)
        {
            return number1 / number2;
        }

        public static double Modulo(double number1, double number2)
        {
            return number1 % number2;
        }

        public static double Exponent(double number1, double number2)
        {
            return Math.Pow(number1, number2);
        }

        public static double NthRoot(double number1, double number2)
        {
            return Math.Pow(number1, 1 / number2);
        }

        public static double Factorial(double number1, double number2 = 0)
        {
            if (number1 <= 1)
                return 1;
            return number1 * Factorial(number1 - 1);
        }

        public static double Abs(double number1, double number2)
        {
            return Math.Abs(number1);
        }

    }
}

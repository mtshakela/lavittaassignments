using System;
using System.Linq;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Square numbers:");
 
            var squareNumbers = (new int[20]).Select((item, index) => {
                var number = index + 1;
                return number * number;
            });

            Console.WriteLine(string.Join(" ", squareNumbers));
            Console.WriteLine();
            var evenNumbers = squareNumbers.Where(x => x % 2 == 0);// Gets list of even numbers
            var oddNumbers = squareNumbers.Where(x => x % 2 != 0);// Gets list of odd numbers


            Console.WriteLine();
            Console.WriteLine("Even numbers");
            Console.WriteLine(string.Join(" ",evenNumbers));
            Console.WriteLine();
            Console.WriteLine("a. Amount of numbers: " + evenNumbers.Count());// Gets count of even numbers.
            Console.WriteLine("b. Total: " + evenNumbers.Sum());// Gets total sum of even numbers.
            Console.WriteLine("c. Average: " + (evenNumbers.Sum()/ evenNumbers.Count()));//Gets average of even numbers.
            Console.WriteLine("d. Minimum: " + evenNumbers.Min());//Gets minimum even number.
            Console.WriteLine("e. Maximum: " + evenNumbers.Max());//Gets maximum even number.

            Console.WriteLine();
            Console.WriteLine("Odd numbers");
            Console.WriteLine(string.Join(" ",oddNumbers));
            Console.WriteLine();
            Console.WriteLine("a. Amount of numbers: " + oddNumbers.Count());// Gets count of even numbers.
            Console.WriteLine("b. Total: " + oddNumbers.Sum());// Gets total sum of even numbers.
            Console.WriteLine("c. Average: " + (oddNumbers.Sum()/ oddNumbers.Count()));//Gets average of even numbers.
            Console.WriteLine("d. Minimum: " + oddNumbers.Min());//Gets minimum even number.
            Console.WriteLine("e. Maximum: " + oddNumbers.Max());//Gets maximum even number.

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;

namespace Homework8.Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<Int32> numberSet = new HashSet<Int32>();
            while (true)
            {
                Console.Write("Enter number: ");
                String value = (Console.ReadLine() ?? String.Empty).Trim();
                if (String.IsNullOrEmpty(value))
                    break;
                Int32 number = Int32.Parse(value);
                if (numberSet.Add(number))
                    Console.WriteLine($"Number {number} is saved in set");
                else
                    Console.WriteLine($"Number {number} is already in set");
                Console.WriteLine("");
            }
            Console.WriteLine("That's all folks !!!");
            Console.ReadLine();
        }
    }
}

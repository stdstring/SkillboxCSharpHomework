using System;
using System.Collections.Generic;

namespace Homework8.Task1
{
    class Program
    {
        private static void ShowNumbers(String description, IList<Int32> numbers)
        {
            Console.WriteLine($"{description}:");
            foreach (Int32 number in numbers)
                Console.Write($"{number} ");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            IList<Int32> numbers = new List<Int32>();
            Random random = new Random();
            for (Int32 number = 0; number < 100; ++number)
                numbers.Add(random.Next(0, 100));
            ShowNumbers("Source numbers", numbers);
            for (Int32 index = 0; index < numbers.Count;)
            {
                if (numbers[index] > 25 && numbers[index] < 50)
                    numbers.RemoveAt(index);
                else
                    ++index;
            }
            ShowNumbers("Dest numbers", numbers);
            Console.WriteLine("That's all folks !!!");
            Console.ReadLine();
        }
    }
}

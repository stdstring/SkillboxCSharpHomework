using System;

namespace Homework4.Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Count: ");
            Int32 count = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Console.WriteLine("Numbers:");
            Int32[] numbers = new Int32[count];
            for (Int32 index = 0; index < count; ++index)
                numbers[index] = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Int32 minNumber = numbers[0];
            for (Int32 index = 1; index < count; ++index)
                minNumber = Math.Min(minNumber, numbers[index]);
            Console.WriteLine($"Min number = {minNumber}");
            Console.ReadLine();
        }
    }
}

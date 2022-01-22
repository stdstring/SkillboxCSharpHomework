using System;

namespace Homework4.Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Max number: ");
            Int32 maxNumber = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Random random = new Random();
            Int32 secret = random.Next(maxNumber);
            while (true)
            {
                Console.Write("Guess: ");
                String value = Console.ReadLine();
                if (String.IsNullOrEmpty(value))
                {
                    Console.Write($"Secret = {secret}");
                    break;
                }

                if (!Int32.TryParse(value, out Int32 guess))
                    Console.WriteLine("Bad value");
                else if (guess == secret)
                {
                    Console.WriteLine("You guessed !!!");
                    break;
                }
                else if (guess < secret)
                    Console.WriteLine("More");
                else
                    Console.WriteLine("Less");
            }
            Console.ReadLine();
        }
    }
}

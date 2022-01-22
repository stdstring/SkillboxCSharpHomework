using System;
using System.Text;

namespace Homework5.Task2
{
    class Program
    {
        private static String[] SplitWords(String source)
        {
            return source.Split(null);
        }

        private static String ReversWords(String inputPhrase)
        {
            String[] words = SplitWords(inputPhrase);
            StringBuilder result = new StringBuilder();
            for (Int32 index = words.Length - 1; index >= 0; --index)
                result.Append($"{words[index]}{(index > 0 ? " ": "")}");
            return result.ToString();
        }

        static void Main(string[] args)
        {
            Console.Write("Input: ");
            String result = ReversWords(Console.ReadLine());
            Console.WriteLine($"Result: {result}");
            Console.ReadLine();
        }
    }
}

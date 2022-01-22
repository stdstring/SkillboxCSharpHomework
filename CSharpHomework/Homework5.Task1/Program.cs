using System;

namespace Homework5.Task1
{
    class Program
    {
        private static String[] SplitWords(String source)
        {
            return source.Split(null);
        }

        private static void ShowWords(String[] words)
        {
            foreach (String word in words)
                Console.WriteLine(word);
        }

        static void Main(string[] args)
        {
            Console.Write("Input: ");
            String[] words = SplitWords(Console.ReadLine());
            Console.WriteLine("Words:");
            ShowWords(words);
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;

namespace Homework8.Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<String, String> phoneBook = new Dictionary<String, String>();
            Console.WriteLine("Fill phone book:");
            while (true)
            {
                Console.Write("Enter phone number: ");
                String phoneNumber = (Console.ReadLine() ?? String.Empty).Trim();
                if (String.IsNullOrEmpty(phoneNumber))
                    break;
                Console.Write("Enter full name: ");
                String fullname = (Console.ReadLine() ?? String.Empty).Trim();
                if (phoneBook.ContainsKey(phoneNumber))
                    phoneBook[phoneNumber] = fullname;
                else
                    phoneBook.Add(phoneNumber, fullname);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Search in phone book:");
            while (true)
            {
                Console.Write("Enter phone number: ");
                String phoneNumber = (Console.ReadLine() ?? String.Empty).Trim();
                if (String.IsNullOrEmpty(phoneNumber))
                    break;
                Console.WriteLine(phoneBook.TryGetValue(phoneNumber, out String fullname) ?
                                  $"Found user \"{fullname}\" by phone number \"{phoneNumber}\"" :
                                  $"Unknown phone number \"{phoneNumber}\"");
            }
            Console.WriteLine("That's all folks !!!");
            Console.ReadLine();
        }
    }
}

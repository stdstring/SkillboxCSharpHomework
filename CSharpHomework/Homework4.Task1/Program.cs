using System;

namespace Homework4.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Row count: ");
            Int32 rowCount = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Console.Write("Column count: ");
            Int32 columnCount = Int32.Parse(Console.ReadLine() ?? String.Empty);
            Int32[,] matrix = new Int32[rowCount, columnCount];
            Random random = new Random();
            for (Int32 row = 0; row < rowCount; ++row)
            {
                for (Int32 column = 0; column < columnCount; ++column)
                {
                    matrix[row, column] = random.Next();
                }
            }
            Int32 sum = 0;
            Console.WriteLine("Matrix :");
            for (Int32 row = 0; row < rowCount; ++row)
            {
                for (Int32 column = 0; column < columnCount; ++column)
                {
                    Console.Write($"{(column > 0 ? ", " : "")}{matrix[row, column]}");
                    sum += matrix[row, column];
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Sum = {sum}");
            Console.ReadLine();
        }
    }
}


using System;

namespace CliApp
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0 || args[0] == null)
            {
                Console.WriteLine("Please provide a file name");
                return 1;
            }
            string fileName = args[0];

            Console.WriteLine($"file name: {fileName}");
            return 0;
        }
    }
}

namespace Exam;

class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("######### EXAM - Parallel and Distributed Systems #########");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("The program will be working with the following amount of Processors " + " -> [" + Environment.ProcessorCount + "]\n");
        Console.ResetColor();

        int[] array = { 1, 3, 12, 6, 5, 7, 9, 24, 2, 4, 6, 17, 8, 10 };
        const int N = 5;

        int? result = FindNumberGreaterThanN(array, N);

        if (result.HasValue)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"First number found greater than {N}: [{result.Value}]");
            Console.WriteLine("->->->->->->->->->->->->->->->->->");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No number found greater than [{N}].");
            Console.ResetColor();
        }
    }

    static int? FindNumberGreaterThanN(int[] array, int N)
    {
        object lockObject = new object();
        int? foundNumber = null;
        ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };

        Parallel.ForEach(array, parallelOptions, (number, state) =>
        {
            if (number > N)
            {
                lock (lockObject)
                {
                    if (foundNumber == null && number > N)
                    {
                        foundNumber = number;
                        state.Stop();
                    }
                }
            }
        });

        return foundNumber;
    }
}


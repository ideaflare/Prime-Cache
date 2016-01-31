using static System.Console;
using System.Linq;

namespace DemoAppCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var primeGen = new PrimeCache.PrimeSequence();

            var primesUnderThousand = primeGen.GetPrimes()
                .TakeWhile(x => x < 1000)
                .ToList();

            primesUnderThousand.ForEach(WriteLine);

            WriteLine("Press any key to exit");
            var na = ReadLine();
        }
    }
}

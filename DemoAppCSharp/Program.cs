using static System.Console;
using System.Linq;

namespace DemoAppCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var primeGen1 = new PrimeCache.PrimeGenerator(new long[] { 2 });
                var na1 = primeGen1.GetCachedPrimes();
            }
            catch (System.ArgumentException e)
            {
                WriteLine($"Caught expected argerrrr: {e.Message}");
            }

            var primesUnder100 = PrimeCache.PrimeGenerator.GeneratePrimes()
                .TakeWhile(x => x < 100)
                .ToList();

            var primeGen = new PrimeCache.PrimeGenerator(primesUnder100);

            var cachedPrimes = primeGen.GetCachedPrimes().Take(100).ToList();

            primesUnder100.ForEach(WriteLine);

            WriteLine("Press any key to exit");
            var na = ReadKey();
        }

        /// <summary>
        /// test
        /// <para>
        /// </para>
        /// </summary>
        /// <returns></returns>
        System.Collections.Generic.IEnumerable<int> xmlTest()
            => Enumerable.Range(0, 100);
    }
}

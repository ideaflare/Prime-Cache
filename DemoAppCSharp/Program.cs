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
                var primeGen1 = new PrimeCache.Generator(new[] { 2 });
                var na1 = primeGen1.GetPrimes();
            }
            catch (System.ArgumentException e)
            {
                WriteLine($"Caught expected argerrrr: {e.Message}");
            }

            var primeGen = new PrimeCache.Generator();

            var primesUnderThousand = primeGen.GetCachedPrimes()
                .TakeWhile(x => x < 100)
                .ToList();

            primesUnderThousand.ForEach(WriteLine);

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

## Prime-Cache
Fast Prime number generator for math problems like project Euler.

[NuGet package avaliable here](https://www.nuget.org/packages/PrimeCache/)

### C# #
```csharp
var primesUnderThousand = PrimeCache.PrimeGenerator.GeneratePrimes()
                .TakeWhile(x => x < 1000)
                .ToList();

primesUnderThousand.ForEach(Console.WriteLine);
```

### F# #
```fsharp
let hundredthPrime = PrimeCache.PrimeGenerator.GeneratePrimes () |> Seq.item 99
```

### Functions:
- GeneratePrimes(), returns a IEnumerable of all prime numbers less than 3037000500.
- GetCachedPrimes(), returns a cached version of GeneratePrimes(), which is keeps calculated primes in memory instead of re-generating them when GetCachedPrimes() is called more than once.

### Constructors:
- Generator(IEnumerable<int> primes) : initializes prime generator with pre-computed primes
- Generator() : calls Generator(new[] { 2 })

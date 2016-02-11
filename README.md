## Prime-Cache
Fast Prime number generator for math problems like project Euler.

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
- GetCachedPrimes(), returns a cached version of GetPrimes(), which is keeps calculated primes in memory instead of re-generating them. 

### Constructors:
- Generator(IEnumerable<int> primes) : initializes prime generator with pre-computed primes
- Generator() : calls Generator(new[] { 2 })

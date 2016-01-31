## Prime-Cache
Simple Prime number generator for math problems like project Euler.

### C# #
```csharp
var primeGen = new PrimeCache.Generator();

var primesUnderThousand = primeGen.GetPrimes()
                .TakeWhile(x => x < 1000)
                .ToList();

primesUnderThousand.ForEach(Console.WriteLine);
```

### F# #
```fsharp
let generator = PrimeCache.Generator()
let hundredthPrime = generator.GetPrimes() |> Seq.item 99
```

### Functions:
- GetPrimes(), returns a IEnumerable of prime numbers
- GetCachedPrimes(), returns a cached version of GetPrimes(), which is keeps calculated primes in memory instead of re-generating them. 

### Constructors:
- Generator(IEnumerable<int> primes) : initializes prime generator with pre-computed primes
- Generator() : calls Generator(new[] { 2 })

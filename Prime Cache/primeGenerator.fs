namespace PrimeCache

/// Prime number generator
type PrimeGenerator private(primes : int64 list, lastPrime) =

    do if(primes.Length < 2 ) then 
        invalidArg "knownPrimes" "Constructor requires at least the first two primes: 2 & 3"

    let cachedPrimes = Seq.cache (GeneratorRebuilder.getPrimesLastPrimeKnown primes lastPrime)
    
    /// Generates prime numbers
    new() = PrimeGenerator([2L;3L],3L)

    /// Initializes prime generator with pre-computed primes
    new(knownPrimes : seq<int64>) =
        let primeList = List.ofSeq knownPrimes
        PrimeGenerator(primeList, List.last primeList)

    /// Get generated IEnumerable<int> sequence of prime numbers
    static member GeneratePrimes () = RollingSieveGenerator.generatePrimes ()

    /// <summary>
    /// Get cached version of GetPrimes()
    /// <para>Keeps calculated primes in memory instead of re-generating them</para>
    /// </summary>
    member this.GetCachedPrimes() = cachedPrimes
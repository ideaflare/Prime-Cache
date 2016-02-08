namespace PrimeCache

/// Prime number generator
type PrimeGenerator private(primes, unit) =
    
    let getCachedPrimes = Seq.cache (RollingSieveGenerator.getPrimes primes)

    let seqLength = 
        primes
        |> Seq.truncate 2
        |> Seq.length

    do if(seqLength < 2 ) then 
        invalidArg "knownPrimes" "Constructor requires at least the first two primes: 2 & 3"
    
    /// Generates prime numbers
    new() = PrimeGenerator({2L..3L},())

    /// Initializes prime generator with pre-computed primes
    new(knownPrimes : seq<int64>) = PrimeGenerator(knownPrimes,())

    /// Get generated IEnumerable<int> sequence of prime numbers
    static member GeneratePrimes() = RollingSieveGenerator.generatePrimes

    /// <summary>
    /// Get cached version of GetPrimes()
    /// <para>Keeps calculated primes in memory instead of re-generating them</para>
    /// </summary>
    member this.GetCachedPrimes() = getCachedPrimes
        

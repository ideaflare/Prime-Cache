namespace PrimeCache

/// Prime number generator
type Generator private(primes, unit) =
    
    let getPrimes = PrimeGenerator.getPrimes (primes)

    let cachedPrimes = Seq.cache getPrimes

    let seqLength = 
        primes
        |> Seq.truncate 2
        |> Seq.length

    do if(seqLength < 2 ) then 
        invalidArg "knownPrimes" "Constructor requires at least the first two primes: 2 & 3"
    
    /// Generates prime numbers
    new() = Generator({2L..3L},())

    /// Initializes prime generator with pre-computed primes
    new(knownPrimes : seq<int64>) = Generator(knownPrimes,())

    /// Get generated IEnumerable<int> sequence of prime numbers
    member this.GetPrimes() = getPrimes

    /// <summary>
    /// Get cached version of GetPrimes()
    /// <para>Keeps calculated primes in memory instead of re-generating them</para>
    /// </summary>
    member this.GetCachedPrimes() = cachedPrimes
        

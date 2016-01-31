namespace PrimeCache

/// Prime number generator
type Generator private(primes)=
    
    let getPrimes = PrimeGenerator.getPrimes primes

    let cachedPrimes = Seq.cache getPrimes

    do if(primes.Length < 2) then 
        invalidArg "knownPrimes" "Constructor requires at least the first two primes: 2 & 3"
    
    /// Generates prime numbers
    new() = Generator([2;3])

    /// Initializes prime generator with pre-computed primes
    new(knownPrimes : seq<int>) = Generator(List.ofSeq knownPrimes)

    /// Get generated IEnumerable<int> sequence of prime numbers
    member this.GetPrimes() = getPrimes

    /// <summary>
    /// Get cached version of GetPrimes()
    /// <para>Keeps calculated primes in memory instead of re-generating them</para>
    /// </summary>
    member this.GetCachedPrimes() = cachedPrimes
        

namespace PrimeCache

/// Prime number generator
type Generator private(primes)=

    let rec isPrime test = function 
        | [] -> true
        | p :: t -> 
            if (test % p = 0) then false
            else isPrime test t
    
    let rec nextPrime primes test = 
        seq { 
            if (isPrime test primes) then 
                yield test
                yield! nextPrime (test :: primes) test
            else yield! nextPrime primes (test + 1)
        }

    let getPrimes =
        let lastPrime = primes |> Seq.last
        seq { 
            yield! primes
            yield! nextPrime primes lastPrime
        }

    let cachedPrimes = Seq.cache getPrimes
    
    /// Generates prime numbers
    new() = Generator([ 2 ])

    /// Initializes prime generator with pre-computed primes
    new(knownPrimes : seq<int>) = Generator(List.ofSeq knownPrimes)

    /// Get generated IEnumerable<int> sequence of prime numbers
    member this.GetPrimes() = getPrimes

    /// <summary>
    /// Get cached version of GetPrimes()
    /// <para>Keeps calculated primes in memory instead of re-generating them</para>
    /// </summary>
    member this.GetCachedPrimes() = cachedPrimes
        

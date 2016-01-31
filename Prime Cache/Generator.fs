namespace PrimeCache

type Generator(primes : int list) = 
    
    let rec isPrime test = 
        function 
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
    
    new() = Generator([ 2 ])
    member this.GetPrimes() = getPrimes
    member this.GetCachedPrimes() = cachedPrimes
        

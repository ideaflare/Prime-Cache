module PrimeGenerator

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
        else yield! nextPrime primes (test + 2)
    }

let getPrimes primes =
    let lastPrime = primes |> Seq.last
    seq { 
        yield! primes
        yield! nextPrime primes lastPrime
    }

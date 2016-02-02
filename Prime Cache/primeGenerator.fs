module PrimeGenerator

let int64root = float >> sqrt >> int64

let isNewPrime test primes = 
        let testMax = int64root test
        primes 
        |> Seq.takeWhile (fun prime -> prime <= testMax)
        |> Seq.exists (fun prime -> test % prime = 0L)
        |> not
        
let rec nextPrime (primes : ResizeArray<int64>) test = 
    seq { 
        if (isNewPrime test primes) then 
            yield test
            primes.Add(test)
        yield! nextPrime primes (test + 2L)
    }

let getPrimes (primes : seq<int64>) = 
    let knownPrimes = ResizeArray<int64>(primes)
    let lastPrime = knownPrimes.[knownPrimes.Count - 1]
    seq { 
        yield! primes
        yield! nextPrime knownPrimes (lastPrime + 2L)
    }



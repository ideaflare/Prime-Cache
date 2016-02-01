module PrimeGenerator

let isNewPrime test primes = 
    not (primes |> Seq.exists (fun prime -> test % prime = 0L))

let int64root = float >> sqrt >> int64

let rec nextPrime (primes : ResizeArray<int64>) test = 
    seq { 
        let testMax = int64root test
        let testPrimes = primes |> Seq.takeWhile (fun prime -> prime <= testMax)

        if (isNewPrime test testPrimes) then 
            yield test
            primes.Add(test)

        yield! nextPrime primes (test + 2L)
    }

let getPrimes primes = 
    let lastPrime = primes |> Seq.last
    let knownPrimes = ResizeArray<int64>(primes)
    seq { 
        yield! primes
        yield! nextPrime knownPrimes (lastPrime + 2L)
    }

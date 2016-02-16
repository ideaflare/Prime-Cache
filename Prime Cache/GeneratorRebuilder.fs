module GeneratorRebuilder

open RollingSieveGenerator

let buildPrimes primes lastPrime =
    let rootLast = int64 (sqrt (float lastPrime))
    let lookup = SieveTable()
    let addSquared, addAndUpdate = primes |> List.partition ((<) rootLast)
    addAndUpdate |> Seq.iter (fun prime ->
        let key = prime + (prime * (lastPrime / prime))
        addOrUpdatePrime lookup key prime
        )
    addSquared |> Seq.iter (fun prime -> lookup.Add(prime * prime,[prime]))
    lookup

let getPrimesLastPrimeKnown primes lastPrime =
    let fillDictionary = async { return buildPrimes primes lastPrime }
    seq { 
        yield! primes
        let lookup = Async.RunSynchronously fillDictionary
        yield! { (lastPrime + 1L) .. System.Int64.MaxValue }
               |> Seq.filter(isPrime lookup)
    }
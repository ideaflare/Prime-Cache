module RollingSieveGenerator

type SieveTable = System.Collections.Generic.Dictionary<int64, int64 list>

let addOrUpdatePrime (lookup: SieveTable) nextLookup prime =
    if(lookup.ContainsKey(nextLookup))
        then lookup.[nextLookup] <-  prime :: lookup.[nextLookup]
        else lookup.[nextLookup] <- [prime]

let movePrimeToNextLookup (lookup: SieveTable) number prime = 
    let nextLookup = prime + number
    addOrUpdatePrime lookup nextLookup prime
    
let isPrime (lookup:SieveTable) number =
    if(lookup.ContainsKey(number))
        then
            let primes = lookup.[number]
            primes |> Seq.iter (movePrimeToNextLookup lookup number)
            lookup.Remove(number) |> ignore
            false
        else 
            let primeSquare = number * number
            lookup.Add(primeSquare, [number])
            true

let generatePrimes () =
    let lookup = SieveTable()
    seq { 2L..System.Int64.MaxValue }
    |> Seq.filter (isPrime lookup)
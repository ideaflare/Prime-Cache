module RollingSieveGenerator

type SieveTable = System.Collections.Generic.Dictionary<int64, int64 list>

let addOrUpdatePrime (lookup: SieveTable) nextLookup prime =
    if(lookup.ContainsKey(nextLookup))
        then lookup.[nextLookup] <-  prime :: lookup.[nextLookup]
        else lookup.[nextLookup] <- [prime]

let carryNumberToPrime (lookup: SieveTable) number prime = 
    let nextLookup = prime + number
    addOrUpdatePrime lookup nextLookup prime
    
let isPrime (lookup:SieveTable) number =
    let square = number * number
    if(lookup.ContainsKey(number))
        then
            let primes = lookup.[number]
            primes |> Seq.iter (carryNumberToPrime lookup number)
            lookup.Remove(number) |> ignore
            false
        else 
            lookup.Add(square, [number])
            true

let generatePrimes =
    let lookup = SieveTable()
    seq { 2L..System.Int64.MaxValue }
    |> Seq.filter (isPrime lookup)

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

let getPrimes primes =
    let knownPrimes = List.ofSeq primes
    let lastPrime = List.last knownPrimes
    let fillDictionary = async { return buildPrimes knownPrimes lastPrime }
    let fillTask = Async.StartAsTask fillDictionary
    seq { 
        yield! knownPrimes
        let lookup = Async.RunSynchronously fillDictionary
        yield! { (lastPrime + 1L) .. System.Int64.MaxValue }
               |> Seq.filter(isPrime lookup)
    }



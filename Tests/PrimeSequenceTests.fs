module PrimeSequenceTests

open Xunit

type GeneratorTests() = 
    let firstTenPrimes = [ 2L; 3L; 5L; 7L; 11L; 13L; 17L; 19L; 23L; 29L ]

    let takeList x sq = sq |> Seq.truncate x |> List.ofSeq

    let emptyGen = PrimeCache.PrimeGenerator.GeneratePrimes
    let initGen = PrimeCache.PrimeGenerator(firstTenPrimes)
    let getStaticPrimes x = PrimeCache.PrimeGenerator.GeneratePrimes () |> takeList x
    
    let getPrimesFrom (initGen : PrimeCache.PrimeGenerator) x =
        initGen.GetCachedPrimes() |> takeList x
    
    [<Fact>]
    member gen.``First 10 primes correctly``() = 
        let generatedPrimes = getStaticPrimes 10
        Assert.Equal<int64 list>(firstTenPrimes, generatedPrimes)

    [<Fact>]
    member gen.``First and Hundreth prime is correct``() = 
        let primes = getPrimesFrom initGen 100
        Assert.Equal(2L, primes.[0])
        Assert.Equal(541L,primes.[99])

    [<Fact>]
    member gen.``Sum of all primes under 2mil is correct``() =
        let primesUnder2MilSum = 
            initGen.GetCachedPrimes()
            |> Seq.takeWhile ((>) 2000000L)
            |> Seq.sum
        Assert.Equal(142913828922L,primesUnder2MilSum)

    [<Fact>]
    member gen.``Successive static calls do not interfere with each other``() =
         let generatedPrimes = getStaticPrimes 1234
         let generatedPrimes2 = getStaticPrimes 1234
         Assert.Equal<int64 list>(generatedPrimes, generatedPrimes2)
    
    [<Fact>]
    member gen.``Empty & Initialized Generator primes are equivalent to statically generated primes``() =
        let emptyGenerated = emptyGen() |> takeList 100
        let initGenerated = getPrimesFrom initGen 100
        let staticGenerated = getStaticPrimes 100
        Assert.Equal<int64 list>(emptyGenerated, initGenerated)
        Assert.Equal<int64 list>(emptyGenerated, staticGenerated)

    [<Fact>]
    member gen.``Initialized generator expects at least two primes``() =
        let onePrime = [2L]
        let illegalConstruct () = 
            let na = PrimeCache.PrimeGenerator(onePrime)
            ()
        let error = Record.Exception illegalConstruct
        Assert.NotNull(error)
        Assert.IsType(typedefof<System.ArgumentException>, error)
            
    [<Fact>]
    member gen.``Cached primes are more than 10x faster on sucessive calls``() = 
        let sw = System.Diagnostics.Stopwatch.StartNew()

        let preCache = initGen.GetCachedPrimes() |> takeList 10000
        let firstTry = sw.ElapsedMilliseconds

        sw.Restart()

        let postCache = initGen.GetCachedPrimes() |> takeList 10000
        let secondTry = sw.ElapsedMilliseconds

        Assert.Equal<int64 list>(preCache, postCache)
        Assert.True((secondTry * 10L) < firstTry)  

    

    

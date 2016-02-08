module PrimeSequenceTests

open Xunit

type GeneratorTests() = 
    let firstTenPrimes = [ 2L; 3L; 5L; 7L; 11L; 13L; 17L; 19L; 23L; 29L ]

    let emptyGen = PrimeCache.PrimeGenerator.GeneratePrimes
    let initGen = PrimeCache.PrimeGenerator(firstTenPrimes)

    let takeList x sq = sq |> Seq.truncate x |> List.ofSeq
    
    let getPrimesFrom (initGen : PrimeCache.PrimeGenerator) x =
        initGen.GetCachedPrimes() |> takeList x
    
    [<Fact>]
    member gen.``Empty Generator generates first 10 primes correctly``() = 
        let generatedPrimes = getPrimesFrom initGen 10
        Assert.Equal<int64 list>(firstTenPrimes, generatedPrimes)
    
    [<Fact>]
    member gen.``Empty Generator primes are equivalent to Initialized``() =
        let emptyGenerated = emptyGen() |> takeList 100
        let initGenerated = getPrimesFrom initGen 100
        Assert.Equal<int64 list>(emptyGenerated, initGenerated)
    
    [<Fact>]
    member gen.``First and Hundreth prime is correct``() = 
        let primes = getPrimesFrom initGen 100
        Assert.Equal(2L, primes.[0])
        Assert.Equal(541L,primes.[99])
    
    [<Fact>]
    member gen.``Cached primes are more than 10x faster on sucessive calls``() = 
        let sw = System.Diagnostics.Stopwatch.StartNew()

        let preCache = initGen.GetCachedPrimes() |> takeList 50000
        let firstTry = sw.ElapsedMilliseconds

        sw.Restart()

        let postCache = initGen.GetCachedPrimes() |> takeList 50000
        let secondTry = sw.ElapsedMilliseconds

        Assert.Equal<int64 list>(preCache, postCache)
        Assert.True((secondTry * 10L) < firstTry)  

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
    member gen.``Generates primes under 2mil quickly``() =
        let underMilSum = 
            initGen.GetCachedPrimes()
            |> Seq.takeWhile ((>) 2000000L)
            |> Seq.sum
        Assert.Equal(142913828922L,underMilSum)

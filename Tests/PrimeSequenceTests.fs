module PrimeSequenceTests

open Xunit

type GeneratorTests() = 
    let emptyGenerator = PrimeCache.Generator()
    let initializedGenerator = PrimeCache.Generator([ 2; 3; 5; 7 ])
    let firstTenPrimes = [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29 ]
    
    let takeList n sequence = 
        sequence
        |> Seq.take n
        |> List.ofSeq
    
    [<Fact>]
    member gen.``Empty Generator generates first 10 primes correctly``() = 
        let generatedPrimes = emptyGenerator.GetPrimes() |> takeList 10
        firstTenPrimes = generatedPrimes |> Assert.True
    
    [<Fact>]
    member gen.``Initialized Generator generates correctly``() = 
        let emptyGenerated = emptyGenerator.GetPrimes() |> takeList 20
        let initGenerated = initializedGenerator.GetPrimes() |> takeList 20
        emptyGenerated = initGenerated |> Assert.True
    
    [<Fact>]
    member gen.``First and Hundreth prime is correct``() = 
        // first
        let first = initializedGenerator.GetPrimes() |> Seq.item 0
        Assert.Equal(2, first)
        // 100th
        let hundredth = initializedGenerator.GetPrimes() |> Seq.item 99
        Assert.Equal(541, hundredth)
    
    [<Fact>]
    member gen.``Cached primes are more than 100x faster on sucessive calls``() = 
        let sw = System.Diagnostics.Stopwatch.StartNew()

        let firstCache = initializedGenerator.GetCachedPrimes() |> takeList 1000
        let firstTry = sw.ElapsedMilliseconds
        sw.Restart()
        let try2 = initializedGenerator.GetCachedPrimes() |> takeList 1000
        let secondTry = sw.ElapsedMilliseconds

        ((secondTry * 100L) < firstTry) |> Assert.True

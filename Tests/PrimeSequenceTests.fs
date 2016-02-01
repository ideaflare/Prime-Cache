module PrimeSequenceTests

open Xunit

type GeneratorTests() = 
    let emptyGenerator = PrimeCache.Generator()
    let initializedGenerator = PrimeCache.Generator([ 2L; 3L; 5L; 7L ])
    let firstTenPrimes = [ 2L; 3L; 5L; 7L; 11L; 13L; 17L; 19L; 23L; 29L ]
    
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
        Assert.Equal(2L, first)
        // 100th
        let hundredth = initializedGenerator.GetPrimes() |> Seq.item 99
        Assert.Equal(541L, hundredth)
    
    [<Fact>]
    member gen.``Cached primes are more than 10x faster on sucessive calls``() = 
        let sw = System.Diagnostics.Stopwatch.StartNew()

        let firstCache = initializedGenerator.GetCachedPrimes() |> takeList 20000
        let firstTry = sw.ElapsedMilliseconds
        sw.Restart()
        let try2 = initializedGenerator.GetCachedPrimes() |> takeList 20000
        let secondTry = sw.ElapsedMilliseconds

        ((secondTry * 10L) < firstTry) |> Assert.True

    [<Fact>]
    member gen.``Initialized generator expects at least two primes``() =
        let onePrime = [2L]
        let illegalConstruct () = 
            let na = PrimeCache.Generator(onePrime)
            ()
        let error = Record.Exception illegalConstruct
        Assert.NotNull(error)
        Assert.IsType(typedefof<System.ArgumentException>, error)

    [<Fact>]
    member gen.``Can generate primes under 2mil quicly``() =
        let max = 2000000L
        let underMil = 
            initializedGenerator.GetPrimes()
            |> Seq.takeWhile ((>) max)
            |> List.ofSeq
        let sum = underMil |> List.sumBy bigint
        Assert.True(max > (List.last underMil))

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Point72.LibraryApi.Endpoints;

namespace Point72.LibraryApi.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class InvertWordsBenchmark
{
    private const string TestString = "The quick brown fox, jumps over the lazy dog!";
    private readonly InvertWordsPreserveStructure _naiveImpl = new();
    private readonly InvertWordsPreserveStructureFast _fastImpl = new();
    
    [Benchmark(Baseline = true)]
    public void NaiveImplementation()
    {
        _naiveImpl.Invert(TestString);
    }
    
    [Benchmark]
    public void Optimized()
    {
        _fastImpl.Invert(TestString);
    }
}
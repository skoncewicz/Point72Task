using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Point72.LibraryApi.Endpoints;

namespace Point72.LibraryApi.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class InvertWordsBenchmark10K
{
    private readonly InvertWordsPreserveStructure _naiveImpl = new();
    private readonly InvertWordsPreserveStructureFast _fastImpl = new();
    
    // according to this site: https://www.oneindia.com/india/dr-vityala-yethindra-a-guinness-world-record-for-the-longest-title-of-a-book-3315149.html
    // longest book title is 3,777

    // unfortunately guinness world record site is not available at the moment
    // but i was able to retrieve cached website that says record now belongs to mister
    // SRINIVASAN N, C. LAKSHMI with 4,558 words
    
    // I think it's safe to say that this challenge might continue, so it might be a good
    // idea to test this on 10k words :)
    
    private static string HugeTitle = string.Join(" ", 
        Enumerable.Repeat("One, two, three, four, five, six, seven, eight, nein, ten!", 1000)
    );
    
    [Benchmark]
    public void NaiveImplementation_Huge()
    {
        _naiveImpl.Invert(HugeTitle);
    }
    
    [Benchmark]
    public void Optimized_Huge()
    {
        _fastImpl.Invert(HugeTitle);
    }
}
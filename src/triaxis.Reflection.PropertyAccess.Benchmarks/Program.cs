using BenchmarkDotNet.Running;

namespace triaxis.Reflection.PropertyAccess.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
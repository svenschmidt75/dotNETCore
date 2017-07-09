using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            Samples().Wait();
        }

        static async Task Samples()
        {
            var xs = Observable.Range(0, 10, ThreadPoolScheduler.Instance);

            Console.WriteLine("Last  = " + await xs);
            Console.WriteLine("First = " + await xs.FirstAsync());
            Console.WriteLine("Third = " + await xs.ElementAt(3));
            Console.WriteLine("All   = " + string.Join(", ", await xs.ToList()));

            try
            {
                Console.WriteLine("Error = " + await xs.Select(x => 1 / (5 - x)));
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Yups, we failed!");
            }
        }
    }
}

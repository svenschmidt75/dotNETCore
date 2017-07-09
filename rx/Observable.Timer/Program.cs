using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IObservable<long> obs = Observable
                                   .Timer(TimeSpan.FromSeconds(2), TimeSpan.FromMilliseconds(100))
                                   .ObserveOn(ThreadPoolScheduler.Instance);
            using (obs.Subscribe(value => {
                Console.WriteLine($"Value: {value}");
            }))
            {
                Thread.Sleep(5000);
            }
            Console.WriteLine("done");
        }
    }
}

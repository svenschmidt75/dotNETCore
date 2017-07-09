using System;
using System.Reactive.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IObservable<int> obs = Observable.Range(0, 10);
            using (obs.Subscribe(value => {
                Console.WriteLine($"Value: {value}");
            }))
            Console.WriteLine("done");
        }
    }
}

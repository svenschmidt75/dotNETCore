using System;
using System.Reactive.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var start = 0;
            var count = 10;
            var max = start + count;
            IObservable<int> obs = Observable.Generate(start, value => value < max, value => value + 1, value => value);
            using (obs.Subscribe(
                value => {
                    // onNext
                    Console.WriteLine($"Value: {value}");
                },
                exception => {
                    // onError
                },
                () => {
                    // onCompleted
                })) {}
                Console.WriteLine("done");
        }
    }
}

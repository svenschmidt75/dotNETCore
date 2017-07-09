using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var observable = Observable.Create<int>(o => {
                o.OnNext(1);
                return Disposable.Empty;
            });
            observable.Subscribe(i => {
                Console.WriteLine(i);
            });
        }
    }
}

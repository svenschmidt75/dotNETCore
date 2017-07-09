using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ConsoleApplication
{
    public static class SampleExtentions
    {
    public static void Dump<T>(this IObservable<T> source, string name)
    {
        source.Subscribe(i => Console.WriteLine("{0}-->{1}", name, i), 
                         ex => Console.WriteLine("{0} failed-->{1}", name, ex.Message),
                        () => Console.WriteLine("{0} completed", name));}
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var numbers = new Subject<int>();
            var scan = numbers.Scan(0, (acc, current) => acc + current);
            numbers.Dump("numbers");
            scan.Dump("scan");
            numbers.OnNext(1);
            numbers.OnNext(2);
            numbers.OnNext(3);
            numbers.OnCompleted();
        }
    }
}
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var subject = new Subject<int>();
            subject.SkipLast(2).Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            Console.WriteLine("Pushing 1");
            subject.OnNext(1);
            Console.WriteLine("Pushing 2");
            subject.OnNext(2);
            Console.WriteLine("Pushing 3");
            subject.OnNext(3);
            Console.WriteLine("Pushing 4");
            subject.OnNext(4);
            subject.OnCompleted();
        }
    }
}

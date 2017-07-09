using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using ConsoleApplication;

namespace EventsAsIObservable
{
    public class Obs4
    {
        public event EventHandler<MyEventArgs> ev;

        public Obs4()
        {
            IObservable<MyEventArgs> observable = Observable.FromEventPattern<MyEventArgs>(
                    addHandler => ev += addHandler, addHandler => ev -= addHandler)
                .Select(pattern => pattern.EventArgs)
                .Where(evt => evt.Value % 2 == 0)
                .ObserveOn(ThreadPoolScheduler.Instance);

            var ev2 = new ManualResetEventSlim();

            IObserver<MyEventArgs> obs = Observer.Create<MyEventArgs>(evt =>
                {
                    Console.WriteLine(evt.Value);
                },
                () =>
                {
                    ev2.Set();
                });

            using (observable.Subscribe(obs))
            {
                foreach (var i in Enumerable.Range(0, 10))
                {
                    ev?.Invoke(this, new MyEventArgs(i));
                }
            }

            obs.OnCompleted();

            ev2.Wait();
        }
    }
}
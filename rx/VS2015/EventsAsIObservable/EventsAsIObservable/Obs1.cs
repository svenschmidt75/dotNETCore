using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace EventsAsIObservable
{
    // 2. Use Where(x => x % 2 == 0).Subscribe etc
    public class Obs1
    {
        public event EventHandler<MyEventArgs> ev;

        public Obs1()
        {
            IObservable<EventPattern<MyEventArgs>> observable = Observable.FromEventPattern<MyEventArgs>(addHandler => ev += addHandler, addHandler => ev -= addHandler);
            using (observable.Subscribe(evt =>
            {
                MyEventArgs eventArgs = evt.EventArgs;
                if (eventArgs.Value % 2 == 0)
                {
                    Console.WriteLine(eventArgs.Value);
                }
            }))
            {
                foreach (var i in Enumerable.Range(0, 10))
                {
                    ev?.Invoke(this, new MyEventArgs(i));
                }
            }
        }
    }
}
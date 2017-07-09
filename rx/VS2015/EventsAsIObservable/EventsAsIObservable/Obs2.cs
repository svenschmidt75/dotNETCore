using System;
using System.Linq;
using System.Reactive.Linq;

namespace EventsAsIObservable
{
    public class Obs2
    {
        public event EventHandler<MyEventArgs> ev;

        public Obs2()
        {
            IObservable<MyEventArgs> observable = Observable.FromEventPattern<MyEventArgs>(
                    addHandler => ev += addHandler, addHandler => ev -= addHandler)
                .Select(pattern => pattern.EventArgs);
            using (observable.Subscribe(evt =>
            {
                if (evt.Value % 2 == 0)
                {
                    Console.WriteLine(evt.Value);
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
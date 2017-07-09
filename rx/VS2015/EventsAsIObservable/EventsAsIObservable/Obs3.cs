using System;
using System.Linq;
using System.Reactive.Linq;

namespace EventsAsIObservable
{
    public class Obs3
    {
        public event EventHandler<MyEventArgs> ev;

        public Obs3()
        {
            IObservable<MyEventArgs> observable = Observable.FromEventPattern<MyEventArgs>(
                    addHandler => ev += addHandler, addHandler => ev -= addHandler)
                .Select(pattern => pattern.EventArgs)
                .Where(evt => evt.Value % 2 == 0);
            using (observable.Subscribe(evt =>
            {
                Console.WriteLine(evt.Value);
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
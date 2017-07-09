using System;

namespace EventsAsIObservable
{
    public class MyEventArgs : EventArgs
    {
        public int Value { get; set; }

        public MyEventArgs(int value)
        {
            Value = value;
        }
    }

    public class Program
    {
        public event EventHandler ev;

        public static void Main(string[] args)
        {
//            var myEvent = new Obs1();
            //var myEvent = new Obs2();
            var myEvent = new Obs3();

            // var myEvent = new Program();
            // myEvent.ev += MyEventHandler;
            // foreach (var i in Enumerable.Range(0, 10))
            // {
            //     myEvent.Trigger(i);
            // }
            // myEvent.ev -= MyEventHandler;
        }

        private static void MyEventHandler(object sender, EventArgs e)
        {
            var eventArgs = (MyEventArgs)e;
            if (eventArgs.Value % 2 == 0)
            {
                Console.WriteLine(eventArgs.Value);
            }
        }

        internal void Trigger(int value)
        {
            ev?.Invoke(this, new MyEventArgs(value));
        }
    }
}

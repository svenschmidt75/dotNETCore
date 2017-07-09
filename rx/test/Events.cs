using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Events {
    public class Events {
        public event EventHandler<EventArgs> ev;

        public static void Foo(ev) {
        }

        private static void Bar(event ev) {
        }
    }
}
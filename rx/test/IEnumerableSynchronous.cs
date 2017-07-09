using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IEnumerableSynchronous {
    public class IEnum {
        public static void Foo(){
            foreach (var item in GetDataTakesALongTime()) {
                DoStuff(item);
            }
        }

        private static IEnumerable<int> GetDataTakesALongTime() {
            foreach (var i in Enumerable.Range(0, 1000)){
                yield return i;
            }
        }

        private static void DoStuff(int item)
        {
            throw new NotImplementedException();
        }
    }
}
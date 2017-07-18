using System;
using System.Collections.Generic;

namespace HashSet
{
    public class HashSet<T> : ICollection<T>
    {
        private List<LinkedList<T>> _data = new List<LinkedList<T>>();

    }
}
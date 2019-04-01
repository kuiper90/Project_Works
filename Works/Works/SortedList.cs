using System;

namespace Works
{
    public class SortedList<T> : Lists_version2<T> where T : IComparable<T>
    {
        public SortedList() : base()
        {
        }

        public new void Add(T element)
        {
            int index = 0;

            while ((index < base.Count) && (obj[index].CompareTo(element) == -1))
                index++;
            base.Insert(index, element);
        }

        public new void Insert(int index, T element)
        {
            base.Insert(index, element);
            Array.Sort(base.obj);
        }

        public new T this[int index]
        {
            set { Add(value); }
        }
    }
}

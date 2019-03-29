using System;
using System.Collections;
using System.Collections.Generic;

namespace Works
{
    public class Lists_version3<T> : IList<T>, IReadOnlyList<T>
    {
        protected T[] obj;
        private const int capacity = 4;

        public Lists_version3()
        {
            this.obj = new T[capacity];
            this.Count = 0;
        }

        public void Add(T element)
        {
            IncreaseSizeIfArrayIsFull(this.obj.Length);
            this.obj[this.Count] = element;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T obiect in obj)
            {
                yield return obiect;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void IncreaseSizeIfArrayIsFull(int value)
        {
            if (this.Count + 1 > capacity)
            {
                T[] extendedObj = new T[this.obj.Length + value];
                Array.Copy(this.obj, extendedObj, capacity);
                this.obj = extendedObj;
            }
        }

        public int Count { get; set; } = 0;

        public bool IsReadOnly { get; }

        public T this[int index]
        {
            get => obj[index];
            set => obj[index] = value;
        }

        public void CopyTo(T[] obj, int objIndex)
        {
            if (obj == null)
                throw new ArgumentNullException("Array is null.");
            if (objIndex < 0)
                throw new ArgumentOutOfRangeException("Index is negative.");            
            if (obj.Length - objIndex < Count)
                throw new ArgumentException("Not enough elements after index in the destination array.");

            for (int i = 0; i < Count; ++i)
                obj[i + objIndex] = this[i];
        }

        public bool Contains(T element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (Equals(this.obj[i], element))
                    return true;
            }
            return false;
        }

        public int IndexOf(T element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (Equals(this.obj[i], element))
                    return i;
                continue;
            }
            return -1;
        }

        public void Insert(int index, T element)
        {
            if (index > this.Count || index < 0)
                throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            IncreaseSizeIfArrayIsFull(this.Count - index);
            Array.Copy(this.obj, index, this.obj, index + 1, this.Count - index);
            this.obj[index] = element;
        }

        public void Clear()
        {
            for (int i = 0; i < this.Count; i++)
                this.obj[i] = default(T);
            this.Count = 0;
        }

        public void ShiftLeft(int index)
        {
            for (int i = index; i < this.Count; i++)
                this.obj[i] = obj[i + 1];
            this.Count--;
        }

        public bool Remove(T element)
        {
            int i = 0;

            while ((i < this.Count) && (!Equals(this.obj[i], element)))
            {
                i++;
            }
            if (i == this.Count)
                return false;
            ShiftLeft(i);
            return true;
        }

        public void RemoveAt(int index)
        {
            ShiftLeft(index);
        }

        //public static bool Equals(T one, T two)
        //{
        //    if (one == null)
        //        return two == null;
        //    if (two == null)
        //        return one == null;
        //    return (object.Equals(one, two));
        //}

        public static void Swap<U>(ref U a, ref U b)
        {
            U temp = a;
            a = b;
            b = temp;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Works
{
    public class Lists<T> : IList<T>
    {
        protected T[] obj;
        private const int capacity = 4;

        public Lists()
        {
            this.obj = new T[capacity];
            this.Count = 0;
            this.IsReadOnly = false;          
        }

        //public Lists(bool isReadOnly)
        //{
        //    this.obj = new T[capacity];
        //    this.Count = 0;
        //    this.IsReadOnly = isReadOnly;
        //}
      
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T obiect in obj)
            {
                yield return obiect;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T element)
        {
            if(this.IsReadOnly)
                throw new NotSupportedException("Array is readonly.");
            IncreaseSizeIfArrayIsFull(this.obj.Length);
            this.obj[this.Count] = element;
            this.Count++;
        }
        public bool IsReadOnly { get; set; }

        private void IncreaseSizeIfArrayIsFull(int value)
        {
            if (this.Count + 1 > obj.Length)
                Array.Resize(ref this.obj, this.obj.Length * 2);
        }

        protected bool IsValidIndex(int index) => 0 <= index && index < this.Count;

        public int Count { get; set; } = 0;


        public T this[int index]
        {
            get => obj[index];
            set => obj[index] = value;
        }

        public bool Contains(T element) => IndexOf(element) >= 0;

        public int IndexOf(T element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (Equals(this.obj[i], element))
                    return i;
            }
            return -1;
        }

        public virtual void Insert(int index, T element)
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("Array is readonly.");
            if (index > this.Count || index < 0)
                throw new ArgumentOutOfRangeException();
            IncreaseSizeIfArrayIsFull(this.Count - index);
            Array.Copy(this.obj, index, this.obj, index + 1, this.Count - index);
            this.obj[index] = element;
            this.Count++;
        }

        public static void Swap<U>(ref U a, ref U b)
        {
            U temp = a;
            a = b;
            b = temp;
        }

        public void Clear()
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("Array is readonly.");
            for (int i = 0; i < this.Count; i++)
                this.obj[i] = default(T);
            this.Count = 0;
        }

        private void ShiftLeft(int index)
        {
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            Array.Copy(this.obj, index + 1, this.obj, index, this.Count - index);
            this.Count--;
        }

        public bool Remove(T element)
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("Array is readonly.");
            int index = IndexOf(element);

            if (IsValidIndex(index))
            {
                ShiftLeft(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("Array is readonly.");
            if (!IsValidIndex(index))
                throw new ArgumentOutOfRangeException();
            if (IsValidIndex(index))
                ShiftLeft(index);
        }

        public static bool Equals(T one, T two) => one.Equals(two);

        public void CopyTo(T[] obj, int objIndex)
        {
            if (obj == null)
                throw new ArgumentNullException("Array is null.");
            if (objIndex < 0)
                throw new IndexOutOfRangeException("Index is negative.");
            if (obj.Length - objIndex < Count)
                throw new ArgumentException("Not enough elements after index in the destination array.");
            for (int i = 0; i < Count; ++i)
                obj[i + objIndex] = this[i];
        }
    }
}

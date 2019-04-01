﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Works
{
    public class Lists<T>  : IEnumerable<T>
    {
        protected T[] obj;
        private const int capacity = 4;

        public Lists()
        {
            this.obj = new T[capacity];
            this.Count = 0;
        }

        //public IEnumerator<T> GetEnumerator() => new EnumeratorGenerics<T>(this);

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
            IncreaseSizeIfArrayIsFull(this.obj.Length);
            this.obj[this.Count] = element;
            this.Count++;
        }

        private void IncreaseSizeIfArrayIsFull(int value)
        {
            if (this.Count + 1 > capacity)
            {
                T[] extendedObj = new T[this.obj.Length + value];
                Array.Copy(this.obj, extendedObj, capacity);
                this.obj = extendedObj;
            }
        }

        public int Count { get; set; } = 0;

        private bool IsValidIndex(int index) => 0 <= index && index < this.Count;

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

        private void ShiftLeft(int index)
        {
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            Array.Copy(this.obj, index + 1, this.obj, index, this.Count - index);
            this.Count--;
        }

        public void Remove(T element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            if (IsValidIndex(index))
                ShiftLeft(index);
        }

        public static bool Equals(T one, T two)
        {
            return one.Equals(two);
        }
    }
}

using System;

namespace Works
{
    public class IntArray
    {
        private int[] array;
        private int count;
        private const int capacity = 4;

        public IntArray()
        {
            this.array = new int[capacity];
            this.count = 0;          
        }

        public void Add(int element)
        {
            IncreaseSizeIfArrayIsFull(this.array.Length);
            this.array[this.count] = element;
            this.count++;
        }

        private void IncreaseSizeIfArrayIsFull(int value)
        {
            if (this.count + 1 > this.array.Length)
            {
                int[] extendedArray = new int[this.array.Length + value];
                Array.Copy(this.array, extendedArray, capacity);
                this.array = extendedArray;
            }
        }

        public int Count() => this.count;

        private bool IsValidIndex(int index) => 0 <= index && index < this.count;
        
        public int Element(int index)
        {
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            return this.array[index];
        }

        public void SetElement(int index, int element)
        {
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("Invalid index: { 0} " + index);
            this.array[index] = element;
        }

        public bool Contains(int element) => IndexOf(element) >= 0;

        public int IndexOf(int element)
        {
            for (int i = 0; i < this.Count(); i++)
            {
                if (this.array[i] == element)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, int element)
        {   
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            IncreaseSizeIfArrayIsFull(this.count - index);
            Array.Copy(this.array, index, this.array, index + 1, this.count - index);
            this.array[index] = element;
        }

        public void Clear()
        {
            this.count = 0;
        }

        private void ShiftLeft(int index)
        {
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            Array.Copy(this.array, index + 1, this.array, index, this.count - index);
            this.count--;
        }

        public void Remove(int element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            if (IsValidIndex(index))
                ShiftLeft(index);
        }
    }
}

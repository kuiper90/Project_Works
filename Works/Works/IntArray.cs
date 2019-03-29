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

        public void IncreaseSizeIfArrayIsFull(int value)
        {
            if (this.count + 1 > capacity)
            {
                int[] extendedArray = new int[this.array.Length + value];
                Array.Copy(this.array, extendedArray, capacity);
                this.array = extendedArray;
            }
        }

        public int Count() => this.count;
        
        public int Element(int index)
        {
            if (!IsValidIndex(index))
                throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            return this.array[index];
        }

        private bool IsValidIndex(int index) => 0 <= index && index < this.count;

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
                continue;
            }
            return -1;
        }

        public void Insert(int index, int element)
        {
            if (index > this.count || index < 0)
                throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            IncreaseSizeIfArrayIsFull(this.count - index);
            Array.Copy(this.array, index, this.array, index + 1, this.count - index);
            this.array[index] = element;
        }

        public void Clear()
        {
            this.count = 0;
        }

        public void ShiftLeft(int index)
        {
            //Array.Copy(this.array, index, this.array, index - 1, this.count - index);
            for (int i = index; i < this.count; i++)
                this.array[i] = array[i + 1];
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

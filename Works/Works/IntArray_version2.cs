using System;

namespace Works
{
    public class IntArray_version2
    {
        private int[] array;
        //private int count;
        private const int capacity = 4;

        public IntArray_version2()
        {
            this.array = new int[capacity];
            this.Count = 0;        
        }

        public void Add(int element)
        {
            IncreaseSizeIfArrayIsFull(this.array.Length);
            this.array[this.Count] = element;
            this.Count++;
        }

        public void IncreaseSizeIfArrayIsFull(int value)
        {
            if (this.Count + 1 > capacity)
            {
                int[] extendedArray = new int[this.array.Length + value];
                Array.Copy(this.array, extendedArray, capacity);
                this.array = extendedArray;
            }
        }

        public int Count { get; set; } = 0;
        //public int Count
        //{
        //    get => this.count;
        //    private set => this.count = value;
        //}

        public int this[int index]
        {
            get { return ((index >= 0) || (index < this.Count)) ? array[index] : Char.MinValue; }
            set
            {
                if ((index >= 0) || (index < this.Count))
                    array[index] = value;
            }
        }

        public void SetElement(int index, int element)
        {
            if (index > this.Count || index < 0)
                throw new IndexOutOfRangeException("Invalid index: { 0} " + index);
            this.array[index] = element;
        }

        public bool Contains(int element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.array[i] == element)
                    return true;
            }
            return false;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.array[i] == element)
                    return i;
                continue;
            }
            return -1;
        }

        public void Insert(int index, int element)
        {
            if (index > this.Count || index < 0)
                throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            IncreaseSizeIfArrayIsFull(this.Count - index);
            Array.Copy(this.array, index, this.array, index + 1, this.Count - index);
            this.array[index] = element;
        }

        public void Clear()
        {
            for (int i = 0; i < this.Count; i++)
                this.array[i] = 0;
            this.Count = 0;
        }

        public void ShiftLeft(int index)
        {
            for (int i = index; i < this.Count; i++)
                this.array[i] = array[i + 1];
            this.Count--;
        }

        public void Remove(int element)
        {
            int i = 0;

            while ((i < this.Count) && (this.array[i] != element))
            {
                i++;
            }
            if (i == this.Count)
                return;
            ShiftLeft(i);
        }

        public void RemoveAt(int index)
        {
            ShiftLeft(index);
        }
    }
}

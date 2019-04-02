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

        private void IncreaseSizeIfArrayIsFull(int value)
        {
            if (this.Count + 1 > this.array.Length)
            {
                int[] extendedArray = new int[this.array.Length + value];
                Array.Copy(this.array, extendedArray, this.array.Length);
                this.array = extendedArray;
            }
        }

        public int Count { get; set; } = 0;
        //public int Count
        //{
        //    get => this.count;
        //    private set => this.count = value;
        //}

        private bool IsValidIndex(int index) => 0 <= index && index < this.Count;

        public int this[int index]
        {
            get { return (IsValidIndex(index)) ? array[index] : Char.MinValue; }
            set
            {
                if (IsValidIndex(index))
                    array[index] = value;
            }
        }

        public void SetElement(int index, int element)
        {
            if (!IsValidIndex(index))
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

        private void ShiftLeft(int index)
        {
            for (int i = index; i < this.Count; i++)
                this.array[i] = array[i + 1];
            this.Count--;
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

using System;
using System.Collections;

namespace Works
{
    public class ObjectArray
    {
        protected object[] obj;
        private const int capacity = 4;

        public ObjectArray()
        {
            this.obj = new object[capacity];
            this.Count = 0;
        }

        public void Add(object element)
        {
            IncreaseSizeIfArrayIsFull(this.obj.Length);
            this.obj[this.Count] = element;
            this.Count++;
        }

        public void IncreaseSizeIfArrayIsFull(int value)
        {
            if (this.Count + 1 > this.obj.Length)
                Array.Resize(ref this.obj, this.obj.Length * 2);
        }

        public int Count { get; set; } = 0;

        public object this[int index]
        {
            get => obj[index];
            set => obj[index] = value;
        }

        public void Insert(int index, object element)
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
                this.obj[i] = 0;
            this.Count = 0;
        }

        public void ShiftLeft(int index)
        {
            for (int i = index; i < this.Count; i++)
                this.obj[i] = obj[i + 1];
            this.Count--;
        }

        public void Remove(object element)
        {
            int i = 0;

            while ((i < this.Count) && (this.obj[i] != element))
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

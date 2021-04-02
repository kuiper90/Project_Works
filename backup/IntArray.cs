using System;

namespace Works
{
    public class IntArray
    {
        int[] arr;
        int count;

        public IntArray()
        {
            int count = 0;
            int size = 4;
            this.arr = new int[size];
        }

        public int Count
        {
            get => this.count;
            private set => this.count = value;
        }

        public virtual void Add(int element)
        {
            CheckCount();
            this.arr[count] = element;
            count++;
        }

        private void CheckCount()
        {
            if (this.count >= this.arr.Length)
            {
                ResizeArray();
            }
        }

        private void ResizeArray()
        {
            int newSize = Math.Max(this.count + 1, this.arr.Length * 2);
            Array.Resize(ref this.arr, newSize);
        }

        public virtual int this[int index]
        {
            get => FindValueUsingIndex(index);
            set => this.arr[index] = value;
        }

        private int FindValueUsingIndex(int index)
            => index < 0 || index >= this.count ? 0 : this.arr[index];

        public bool Contains(int element)
            => IndexOf(element) >= 0;

        public int IndexOf(int element)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (this.arr[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, int element)
        {
            if (index > this.count)
            {
                return;
            }

            CheckCount();
            ExpandCount(index);
            this.arr[index] = element;
            count++;
        }

        void ExpandCount(int index)
        {
            for (int i = this.count; i > index; i--)
            {
                this.arr[i] = this.arr[i - 1];
            }
        }

        public void Clear()
        {
            Array.Resize(ref this.arr, 0);
            this.count = 0;
        }

        public void Remove(int element)
        {
            int index = IndexOf(element);
            if (index < 0)
            {
                return;
            }

            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (0 <= index && index < this.count)
            {
                ShrinkCount(index);
                this.count--;
            }
        }

        void ShrinkCount(int index)
        {
            for (int i = index; i < this.count - 1; i++)
            {
                this.arr[i] = this.arr[i + 1];
            }
        }
    }
}

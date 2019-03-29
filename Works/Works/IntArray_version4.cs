using System;

namespace Works
{
    public class IntArray_version4 : ObjectArray
    {
        public IntArray_version4() : base()
        {
        }

        public void Add(int element)
        {
            object obj = element;
            base.Add(obj);
        }

        public new int this[int index]
        {
            get => (int)base.obj[index];
            set => base.obj[index] = value;
        }

        public bool Contains(int element)
        {   
            for (int i = 0; i < this.Count; i++)
            {
                if ((int)this.obj[i] == element)
                    return true;
            }
            return false;
        }

        public int IndexOf(int element)
        {
            object obj = element;
            for (int i = 0; i < this.Count; i++)
            {
                if ((int)this.obj[i] == (int)obj)
                    return i;
                continue;
            }
            return -1;
        }

        //public bool Contains(int element)
        //{
        //    object obj = element;
        //    return base.Contains(obj);
        //}

        //public int IndexOf(int element)
        //{
        //    object obj = element;
        //    return base.IndexOf(obj);
        //}

        public void Insert(int index, int element)
        {
            object obj = element;
            base.Insert(index, obj);
        }

        public void Remove(int element)
        {
            object obj = element;
            base.Remove(obj);
        }
    }
}

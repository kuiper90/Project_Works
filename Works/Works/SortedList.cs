using System;

namespace Works
{
    public class SortedList<T> : Lists_version2<T> where T : IComparable<T>
    {
        public SortedList() : base()
        {
        }

        public new void Add(T element)
        {
            int index = 0;

            while ((index < base.Count) && (obj[index].CompareTo(element) == -1))
                index++;
            base.Insert(index, element);
        }

        private bool CheckIfSorted(int index, T element)
        {
            if ((index == 0) && (obj[index + 1].CompareTo(element) < 0))
                return false;
            if ((index == base.Count - 1) && (obj[index - 1].CompareTo(element) > 0))
                return false;
            if ((obj[index - 1].CompareTo(element) > 0) || (obj[index + 1].CompareTo(element) < 0))
                return false;
            return true;
        }

        public override void Insert(int index, T element)
        {
            if (!IsValidIndex(index))
                throw new Exception("Invalid index.");
            if (!CheckIfSorted(index, element))
                throw new Exception("Inserting element would result in an unsorted list.");
            base.Insert(index, element);

            //    if (!IsValidIndex(index))
            //      throw new IndexOutOfRangeException("Invalid index: {0} " + index);
            //    if (index > 0)
            //    {
            //      if ((obj[index - 1].CompareTo(element) < 0) && (obj[index + 1].CompareTo(element) > 0))
            //          base.Insert(index, element);
            //      else
            //          throw new Exception("Inserting element would result in an unsorted list.");
            //    }
            //    else if ((index == 0) && (obj[index + 1].CompareTo(element) > 0))
            //      base.Insert(index, element);
        }

        public new T this[int index]
        {
            get => obj[index];
            set
            {
                if (!IsValidIndex(index))
                    throw new Exception("Invalid index.");
                if (!CheckIfSorted(index, value))
                    throw new Exception("Inserting element would result in an unsorted list.");
                Add(value);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Works
{
    public class SortedList<T> : Lists<T> where T : IComparable<T>
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

        private bool CheckIfNullOrEmpty<T>(T element) => ((ReferenceEquals(element, "")) || (EqualityComparer<T>.Default.Equals(element, default(T))));

        private bool CheckIfSorted(int index, T element)
        {   
            if ((index == 0) && (obj[index + 1].CompareTo(element) < 0))
                return false;
            if (((index > 0) && (index == base.Count - 1)) && (obj[index - 1].CompareTo(element) > 0))
                return false;
            if ((index > 0) && (obj[index - 1].CompareTo(element) > 0) || (obj[index + 1].CompareTo(element) < 0))
                return false;
            return true;
        }

        public override void Insert(int index, T element)
        {
            if (!IsValidIndex(index))
                throw new Exception("Invalid index.");
            if (CheckIfNullOrEmpty(element))
                throw new Exception("Element is null or empty.");
            if (!CheckIfSorted(index, element))
                throw new Exception("Inserting element would result in an unsorted list.");
            base.Insert(index, element);
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

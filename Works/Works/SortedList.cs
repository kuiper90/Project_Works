using System;
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
            base.CheckIfReadOnly();
            int index = 0;
            while ((index < base.Count) && (obj[index].CompareTo(element) == -1))
                index++;
            base.Insert(index, element);
        }

        private bool IsNullOrEmpty(T element) => ((ReferenceEquals(element, "")) || 
            (EqualityComparer<T>.Default.Equals(element, default(T))));

        private bool IsSorted(int index, T element)
        {   
            if ((index == 0) && (obj[index + 1].CompareTo(element) < 0))
                return false;
            if (((index > 0) && (index == base.Count)) && (obj[index - 1].CompareTo(element) > 0))
                return false;
            if (((0 < index ) && (index < base.Count)) && ((obj[index - 1].CompareTo(element) > 0  || (obj[index + 1].CompareTo(element) < 0))))
                return false;
        return true;
        }

        public override void Insert(int index, T element)
        {
            base.CheckIfReadOnly();
            base.CheckIfValidIndex(index);
            CheckIfNullOrEmpty(element);
            CheckIfSorted(index, element);
            base.Insert(index, element);
        }

        public new T this[int index]
        {
            get
            {
                base.CheckIfValidIndex(index);
                return obj[index];
            }
            set
            {
                base.CheckIfReadOnly();
                base.CheckIfValidIndex(index);
                CheckIfSorted(index, value);
                Add(value);
            }
        }
        
        private void CheckIfSorted(int index, T element)
        {
            if (!IsSorted(index, element))
                throw new Exception("Inserting element would result in an unsorted list.");
        }

        private void CheckIfNullOrEmpty(T element)
        {
            if (IsNullOrEmpty(element))
                throw new DoesNotComplyWithSortedState("Element is null or empty.");
        }
    }
}
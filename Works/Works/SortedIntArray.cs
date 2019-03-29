namespace Works
{
    public class SortedIntArray : IntArray_version3
    {
        public SortedIntArray(int[] array) : base()
        {  
        }

        public new void Add(int element)
        {
            int index = 0;

            while ((index < base.Count) && (element > array[index]))
                index++;
            base.Insert(index, element);
        }
    }
}

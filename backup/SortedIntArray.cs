namespace Works
{
    public class SortedIntArray : IntArray
    {
        public override int this[int index]
        {
            set
            {
                if (this.ElementAt(index - 1, value) <= value && value <= this.ElementAt(index + 1, value))
                {
                    base[index] = value;
                }
            }
        }

        private int ElementAt(int index, int std)
            => index >= 0 && index < this.Count ? base[index] : std;

        public override void Add(int element)
        {
            base.Add(element);
            this.Sort();
        }

        public void Sort()
        {
            bool isSorted = true;
            while (isSorted)
            {
                isSorted = false;

                for (int i = 0; i < this.Count - 1; i++)
                {
                    if (this[i] > this[i + 1])
                    {
                        this.SwitchValues(i);
                        isSorted = true;
                    }
                }
            }
        }

        private void SwitchValues(int i)
        {
            int temp = base[i];
            base[i] = base[i + 1];
            base[i + 1] = temp;
        }

        public new void Insert(int index, int element)
        {
            if (this.ElementAt(index - 1, element) <= element && element <= this.ElementAt(index, element))
            {
                base.Insert(index, element);
            }

        }
    }
}

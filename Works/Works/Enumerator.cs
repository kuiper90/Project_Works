using System;
using System.Collections;

namespace Works
{
    class Enumerator : IEnumerator
    {
        private ObjectArray_version2 objArr;
        private int index;

        public Enumerator(ObjectArray_version2 objArr)
        {
            this.objArr = objArr;
            this.index = -1;
        }

        public bool MoveNext()
        {
            if (index >= objArr.Count - 1)
                return false;
            return (++index >= objArr.Count);
        }

        public void Reset()
        {
            index = -1;
        }

        public object Current
        {
            get
            {
                if (index == -1)
                    throw new InvalidOperationException("Enumeration not started.");
                if (index == objArr.Count)
                    throw new InvalidOperationException("Past end of list.");
                return objArr[index];
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Works
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public struct Entry
        {
            public int hashCode;
            public int next;
            public TKey key;
            public TValue value;
        }

        private int[] buckets;
        private Entry[] entries;
        private int posInUse;
        private int firstFreePosition;
        private int posRemoved;
        private IEqualityComparer<TKey> comp;

        public Dictionary() : this(0, null) { }

        public Dictionary(int capacity) : this(capacity, null) { }

        public Dictionary(IEqualityComparer<TKey> comp) : this(0, comp) { }

        public Dictionary(int capacity, IEqualityComparer<TKey> comp)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(null, "Negative capacity.");
            if (capacity > 0)
                Initialize(capacity);
            this.comp = comp ?? EqualityComparer<TKey>.Default;
        }

        private Dictionary(IDictionary<TKey, TValue> dict) : this(dict, null) { }

        public Dictionary(IDictionary<TKey, TValue> dict, IEqualityComparer<TKey> comp) :
            this(dict != null ? dict.Count : 0, comp)
        {
            if (dict == null)
                throw new ArgumentNullException(null, "Dictionary is null.");
            foreach (KeyValuePair<TKey, TValue> pair in dict)
                Add(pair.Key, pair.Value);
        }

        //public IEqualityComparer<TKey> Comparer { get => comp; }

        public int Count { get => posInUse - posRemoved; }

        public Entry[] Entries { get => entries; }

        public TValue this[TKey key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0)
                    return entries[i].value;
                return default(TValue);
            }
            set
            {
                Insert(key, value, false, false);
            }
        }

        //Replacement for: L 61 - 69
        //public TValue GetValue(TKey key)
        //{
        //    int i;
        //    if ((i = FindEntry(key)) != -1)
        //        throw new KeyNotFoundException("Key not found.");
        //    if (i >= 0)
        //        return entries[i].value;
        //    return default(TValue);
        //}

        private List<TKey> GetKeyList()
        {
            List<TKey> keyList = new List<TKey>();

            foreach (Entry entry in entries)
            {
                keyList.Add(entry.key);
            }
            return keyList;
        }

        public ICollection<TKey> Keys
        {
            get => GetKeyList();
        }

        private List<TValue> GetValueList()
        {
            List<TValue> valueList = new List<TValue>();

            foreach (Entry entry in entries)
            {
                valueList.Add(entry.value);
            }
            return valueList;
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get => GetValueList();
        }

        public void Add(TKey key, TValue value)
        {
            Insert(key, value, true, false);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Add(keyValuePair.Key, keyValuePair.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int i = FindEntry(keyValuePair.Key);

            return i >= 0 && EqualityComparer<TValue>.Default.Equals(entries[i].value, keyValuePair.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int i = FindEntry(keyValuePair.Key);

            if (i >= 0 && EqualityComparer<TValue>.Default.Equals(entries[i].value, keyValuePair.Value))
            {
                Remove(keyValuePair.Key);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            if (posInUse > 0)
            {
                for (int i = 0; i < buckets.Length; i++)
                    buckets[i] = -1;
                Array.Clear(entries, 0, posInUse);
                firstFreePosition = -1;
                posInUse = 0;
                posRemoved = 0;
            }
        }

        public bool ContainsKey(TKey key) => FindEntry(key) >= 0;

        //public bool ContainsValue(TValue value)
        //{
        //    if (value == null)
        //    {
        //        for (int i = 0; i < posInUse; i++)
        //        {
        //            if ((entries[i].hashCode >= 0) && (entries[i].value == null))
        //                return true;
        //        }
        //    }
        //    else
        //    {
        //        EqualityComparer<TValue> comp = EqualityComparer<TValue>.Default;
        //        for (int i = 0; i < posInUse; i++)
        //        {
        //            if ((entries[i].hashCode >= 0) && comp.Equals(entries[i].value, value))
        //                return true;
        //        }
        //    }
        //    return false;
        //}

        private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(null, "Array of values is null.");
            if (index < 0 || index > array.Length)
                throw new ArgumentOutOfRangeException(null, "Index out of range.");
            if (array.Length - index < Count)
                throw new ArgumentException("Not enough elements after index in the destination array.");

            int size = this.posInUse;
            Entry[] entries = this.entries;
            for (int i = 0; i < size; i++)
            {
                if (entries[i].hashCode >= 0)
                    array[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);                
            }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            int i = 0;
            do
            {
                yield return new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                i++;
            } while (i < entries.Length);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            int i = 0;
            do
            {
                yield return new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                i++;
            } while (i < entries.Length);
        }

        private int FindEntry(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(null, "Key is null.");
            if (buckets != null)
            {
                int hashCode = Math.Abs(comp.GetHashCode(key));
                int i = buckets[hashCode % buckets.Length];
                while (i >= 0)
                {
                    if (entries[i].hashCode == hashCode && comp.Equals(entries[i].key, key))
                        return i;
                    i = entries[i].next;
                }
            }
            return -1;
        }

        private void Initialize(int capacity)
        {
            int size = GetPrime(capacity);
            buckets = new int[size];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = -1;
            entries = new Entry[size];
            firstFreePosition = -1;
        }

        public void Insert(TKey key, TValue value, bool add, bool execFlag)
        {
            if (key == null)
                throw new ArgumentNullException(null, "Key is null.");

            if (buckets == null)
                Initialize(0);
            int hashCode = Math.Abs(comp.GetHashCode(key));
            int destBucket = hashCode % buckets.Length;

            for (int i = buckets[destBucket]; i >= 0; i = entries[i].next)
            {
                if (execFlag)
                {
                    if (entries[i].hashCode == hashCode && comp.Equals(entries[i].key, key))
                    {
                        if (add)
                            throw new ArgumentException("Adding duplicate.");
                        entries[i].value = value;
                        return;
                    }
                }
                else
                {
                    if (comp.Equals(entries[i].key, key))
                    {
                        if (add)
                            throw new ArgumentException("Adding duplicate.");
                        entries[i].value = value;
                        return;
                    }
                }
            }
            int index;
            if (posRemoved > 0)
            {
                index = firstFreePosition;
                firstFreePosition = entries[index].next;
                posRemoved--;
            }
            else
            {
                if (posInUse == entries.Length)
                {
                    Resize();
                    destBucket = hashCode % buckets.Length;
                }
                index = posInUse;
                posInUse++;
            }
            entries[index].hashCode = hashCode;
            entries[index].next = buckets[destBucket];
            entries[index].key = key;
            entries[index].value = value;
            buckets[destBucket] = index;
        }

        private void Resize()
        {
            Resize(ExpandPrime(posInUse));
        }

        private void Resize(int newSize)
        {
            if (newSize < entries.Length)
                throw new ArgumentException("New size not long enough.");
            int[] newBuckets = new int[newSize];

            for (int i = 0; i < newBuckets.Length; i++)
                newBuckets[i] = -1;

            Entry[] newEntries = new Entry[newSize];

            Array.Copy(entries, 0, newEntries, 0, posInUse);
            for (int i = 0; i < posInUse; i++)
            {
                if (newEntries[i].hashCode >= 0)
                {
                    int bucket = newEntries[i].hashCode % newSize;
                    newEntries[i].next = newBuckets[bucket];
                    newBuckets[bucket] = i;
                }
            }
            buckets = newBuckets;
            entries = newEntries;
        }

        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(null, "Key is null.");
            if (buckets != null)
            {
                int hashCode = Math.Abs(comp.GetHashCode(key));
                int currentBucket = hashCode % buckets.Length;
                int last = -1;
                for (int i = buckets[currentBucket]; i >= 0; last = i, i = entries[i].next)
                {
                    if (entries[i].hashCode == hashCode && comp.Equals(entries[i].key, key))
                    {
                        if (last < 0)                       
                            buckets[currentBucket] = entries[i].next;
                        else
                            entries[last].next = entries[i].next;
                        entries[i].hashCode = -1;
                        entries[i].next = firstFreePosition;
                        entries[i].key = default(TKey);
                        entries[i].value = default(TValue);
                        firstFreePosition = i;
                        posRemoved++;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int i = FindEntry(key);
            if (i >= 0)
            {
                value = entries[i].value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        public TValue GetValueOrDefault(TKey key)
        {
            int i = FindEntry(key);
            if (i >= 0)
            {
                return entries[i].value;
            }
            return default(TValue);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            CopyTo(array, index);
        }

        private int ExpandPrime(int sizeOne)
        {
            int sizeTwo = 2 * sizeOne;

            if ((uint)sizeTwo > 0x7FEFFFFD && 0x7FEFFFFD > sizeOne)
            {
                return 0x7FEFFFFD;
            }
            return GetPrime(sizeTwo);
        }

        private int GetPrime(int min)
        {
            if (min < 0)
                throw new ArgumentOutOfRangeException(null, "Parameter is null.");

            if (primes[primes.Length - 1] < min)
                RecalculatePrime(primes[primes.Length - 1], min * 2);
            for (int i = 0; i < primes.Length; i++)
            {
                int prime = primes[i];
                if (prime >= min)
                    return prime;
            }

            //for (int i = (min | 1); i < Int32.MaxValue; i += 2)
            //{
            //    if (IsPrime(i) && ((i - 1) % Hashtable.HashPrime != 0))
            //        return i;
            //}
            return min;
        }

        public int[] primes = CalculatePrime(7199369);

        private static int[] CalculatePrime(int limit)
        {
            int k = limit / 2;
            int max = 0;
            int tmp = 0;
            BitArray isPrime = new BitArray(limit);
            isPrime.SetAll(true);

            for (int i = 1; i < k; i++)
            {
                tmp = (i << 1) + 1;
                max = (k - i) / tmp;
                for (int j = i; j <= max; j++)
                    isPrime[i + j * tmp] = false;
            }

            int totalCount = CalculateNrOfPrimes(isPrime);
            int[] res = new int[totalCount];
            int n = 0;

            if (totalCount > 2)
                res[n++] = 2;
            for (int i = 1; i < limit; i++)
            {
                if (isPrime[i])
                    res[n++] = i * 2 + 1;
            }
            return res;
            
        }

        private static int CalculateNrOfPrimes(BitArray isPrime)
        {
            int totalCount = 0;

            for (int i = 1; i < isPrime.Length; i++)
            {
                if (isPrime[i])
                    totalCount++;
            }
            return totalCount + 1;
        }

        private void RecalculatePrime(int limitOne, int limitTwo)
        {
            double rt = Math.Ceiling(Math.Sqrt(limitTwo));            
            BitArray isPrime = new BitArray(limitTwo);
            isPrime.SetAll(true);

            for (int i = limitOne; i <= rt; ++i)
            {
                for (int j = 0; primes[j] <= Math.Sqrt(i); j++)
                {
                    if (i % primes[j] == 0)
                        isPrime[i] = false;
                }
            }
            
            int[] extraPrimes = CopyToArray(isPrime);
            Array.Resize(ref primes, primes.Length + extraPrimes.Length);
            Array.Copy(extraPrimes, 0, primes, primes.Length, extraPrimes.Length);
            //primes = primes.Concat(extraPrimes).ToArray();
        }


        private int[] CopyToArray(BitArray isPrime)
        {            
            int[] res = new int[CalculateNrOfPrimes(isPrime)];
            int j = 0;
            
            for (int i = 1; i < isPrime.Length + 1; i++)
            {
                if (isPrime[i])
                    res[j++] = i;
            }
            return res;
        }
    }
}
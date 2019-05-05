using System;
using System.Collections;

namespace Works
{
    public class PrimeNumbers
    {
        public static int ExpandPrime(int sizeOne)
        {
            int sizeTwo = 2 * sizeOne;

            if ((uint)sizeTwo > 0x7FEFFFFD && 0x7FEFFFFD > sizeOne)
            {
                return 0x7FEFFFFD;
            }
            return GetPrime(sizeTwo);
        }

        public static int GetPrime(int min)
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

        public static int[] primes = CalculatePrime(7199369);

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

        private static void RecalculatePrime(int limitOne, int limitTwo)
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

        private static int[] CopyToArray(BitArray isPrime)
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

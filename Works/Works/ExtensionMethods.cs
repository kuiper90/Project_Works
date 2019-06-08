using System;
using System.Collections;
using System.Collections.Generic;

namespace Works
{
    public static class ExtensionMethods
    {
        public static bool AllQ<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            foreach (TSource current in source)
            {
                if (!predicate(current))
                    return false;
            }
            return true;
        }

        public static bool AnyQ<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            foreach (TSource current in source)
            {
                if (predicate(current))
                    return true;
            }
            return false;
        }

        public static TSource FirstQ<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            foreach (TSource current in source)
            {
                if (predicate(current))
                    return current;
            }
            return default(TSource);
        }

        public static IEnumerable<TResult> SelectQ<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");
            foreach (TSource current in source)
            {
                yield return selector(current);
            }
        }

        public static IEnumerable<TResult> SelectManyQ<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");
            foreach (TSource current in source)
            {
                foreach (TResult thisSource in selector(current))
                    yield return thisSource;
            }
        }

        public static IEnumerable<TSource> WhereQ<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("selector");
            List<TSource> rez = new List<TSource>();
            foreach (TSource current in source)
            {
               if (predicate(current))
                    rez.Add(current);
            }
            return rez;
        }

        public static Dictionary<TKey, TElement> ToDictionaryQ<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (keySelector == null)
                throw new ArgumentNullException("selector");
            if (elementSelector == null)
                throw new ArgumentNullException("selector");
            Dictionary<TKey, TElement> dict = new Dictionary<TKey, TElement>();
            foreach (TSource current in source)
            {
                    dict.Add(keySelector(current), elementSelector(current));
            }
            return dict;
        }

        public static IEnumerable<TResult> ZipQ<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first == null)
                throw new ArgumentNullException("source");
            if (second == null)
                throw new ArgumentNullException("selector");
            if (resultSelector == null)
                throw new ArgumentNullException("selector");
            IEnumerator<TFirst> e1 = first.GetEnumerator();
            IEnumerator<TSecond> e2 = second.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext())
                yield return resultSelector(e1.Current, e2.Current);
        }

        public static TAccumulate AggregateQ<TSource, TAccumulate>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (seed == null)
                throw new ArgumentNullException("selector");
            if (func == null)
                throw new ArgumentNullException("selector");
            foreach (TSource current in source)
            {
                seed = func(seed, current);
            }
            return seed;
        }
    }
}

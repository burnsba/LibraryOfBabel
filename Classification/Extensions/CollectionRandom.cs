using Classification.Rng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Extensions
{
    public static class CollectionRandom
    {
        public static T RandHashSetValue<T>(this HashSet<T> d, Random r)
        {
            List<T> dlist = d.ToList();
            return dlist[r.Next(dlist.Count)];
        }

        public static T RandDictValue<TKey, T>(Dictionary<TKey, IEnumerable<T>> d, Random r)
        {
            int safety = 0;

            var keyList = d.Keys.ToList();
            var randKey = keyList[r.Next(keyList.Count)];

            List<T> dlist = d[randKey].ToList();

            while (dlist.Count == 0)
            {
                randKey = keyList[r.Next(keyList.Count)];
                dlist = d[randKey].ToList();
                safety++;

                if (safety > 100)
                {
                    throw new TimeoutException("Could not find not empty set in dictionary");
                }
            }

            return dlist[r.Next(dlist.Count)];
        }

        public static T RandDictValue<TKey, T>(Dictionary<TKey, HashSet<T>> d, Random r)
        {
            int safety = 0;

            var keyList = d.Keys.ToList();
            var randKey = keyList[r.Next(keyList.Count)];

            List<T> dlist = d[randKey].ToList();

            while (dlist.Count == 0)
            {
                randKey = keyList[r.Next(keyList.Count)];
                dlist = d[randKey].ToList();
                safety++;

                if (safety > 100)
                {
                    throw new TimeoutException("Could not find not empty set in dictionary");
                }
            }

            return dlist[r.Next(dlist.Count)];
        }

        public static T RandHashSetValue<T>(this HashSet<T> d, StandardRandom r)
        {
            List<T> dlist = d.ToList();
            return dlist[(int)r.Next(dlist.Count)];
        }

        public static T RandDictValue<TKey, T>(Dictionary<TKey, IEnumerable<T>> d, StandardRandom r)
        {
            int safety = 0;

            var keyList = d.Keys.ToList();
            var randKey = keyList[(int)r.Next(keyList.Count)];

            List<T> dlist = d[randKey].ToList();

            while (dlist.Count == 0)
            {
                randKey = keyList[(int)r.Next(keyList.Count)];
                dlist = d[randKey].ToList();
                safety++;

                if (safety > 100)
                {
                    throw new TimeoutException("Could not find not empty set in dictionary");
                }
            }

            return dlist[(int)r.Next(dlist.Count)];
        }

        public static T RandDictValue<TKey, T>(Dictionary<TKey, HashSet<T>> d, StandardRandom r)
        {
            int safety = 0;

            var keyList = d.Keys.ToList();
            var randKey = keyList[(int)r.Next(keyList.Count)];

            List<T> dlist = d[randKey].ToList();

            while (dlist.Count == 0)
            {
                randKey = keyList[(int)r.Next(keyList.Count)];
                dlist = d[randKey].ToList();
                safety++;

                if (safety > 100)
                {
                    throw new TimeoutException("Could not find not empty set in dictionary");
                }
            }

            return dlist[(int)r.Next(dlist.Count)];
        }
    }
}

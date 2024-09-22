using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    public class CollectionUtility
    {
        public static T? SpiralFindNext<T>(IList<T> collection, int startIndex, Func<T, bool> predicate, int maxSteps = 0)
        {
            var len = collection.Count();
            int endSteps = len - startIndex;
            int steps = len;
            if (endSteps > len)
            {
                steps = endSteps;
            }

            if (predicate(collection[startIndex]))
            {
                return collection[startIndex];
            }

            for (int i = 0; i < steps; i++)
            {
                int offset;

                offset = startIndex + i;
                if (offset < len)
                {
                    if (predicate(collection[offset]))
                    {
                        return collection[offset];
                    }
                }

                offset = startIndex - i;

                if (offset >= 0)
                {
                    if (predicate(collection[offset]))
                    {
                        return collection[offset];
                    }
                }

                if (maxSteps > 0 && i >= maxSteps)
                {
                    return default(T);
                }
            }

            return default(T);
        }

    }
}

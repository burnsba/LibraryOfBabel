using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification
{
    public class Stat<T> where T : struct
    {
        public T Min { get; set; }
        public T Max { get; set; }
        public double Average { get; set; }

        public Stat()
        {
            Min = default(T);
            Max = default(T);
        }

        public override string ToString()
        {
            return $"{Min} - {Max} // {Average}";
        }
    }
}

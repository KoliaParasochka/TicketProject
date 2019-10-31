using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISorter<T>
    {
        /// <summary>
        /// Sorting data
        /// </summary>
        /// <param name="list">the list of data</param>
        /// <returns>sorted list</returns>
        IEnumerable<T> Sort(List<T> list);
    }
}

using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class StationService : ISorter<Station>
    {
        IUnitOfWork repository;

        public StationService(IUnitOfWork unit)
        {
            if (unit != null)
            {
                repository = unit;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Sorting data
        /// </summary>
        /// <param name="list">the list of data</param>
        /// <returns>sorted list</returns>
        public IEnumerable<Station> Sort(List<Station> list)
        {
            list.Sort();
            return list;
        }
    }
}

using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRouteService
    {
        /// <summary>
        /// Searching routes
        /// </summary>
        /// <param name="s1">the first station</param>
        /// <param name="s2">the last station</param>
        /// <returns>The list of Routes which inlsudes s1 and s2.</returns>
        Task<IEnumerable<RouteViewModel>> Search(List<RouteViewModel> list, string s1, string s2);
    }
}

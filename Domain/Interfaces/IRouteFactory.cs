using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRouteFactory
    {
         RouteInfo GetRoute(int? id);
    }
}

using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class RouteService
    {
        IUnitOfWork repository;

        public RouteService(IUnitOfWork unit)
        {
            if(unit != null)
            {
                repository = unit;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Creates the list of RouteViewModels
        /// </summary>
        /// <returns></returns>
        public async Task<List<RouteViewModel>> GetRoutes()
        {
            List<RouteViewModel> resultList = new List<RouteViewModel>();
            List<Route> routesAndStations = (List<Route>)await repository.Routes.GetAllAsync(el => el.Stations);
            foreach(var el in routesAndStations)
            {
                resultList.Add(new RouteViewModel
                {
                    Id = el.Id,
                    StartStation = el.Stations.First.Value.Name,
                    StartDate = el.Stations.First.Value.DepartureTime,
                    FinishDate = el.Stations.Last.Value.ArrivingTime,
                    FinishStation = el.Stations.Last.Value.Name,
                    TravelTime = GetTravelTime(el.Stations.First.Value.DepartureTime, el.Stations.Last.Value.ArrivingTime)
                });
            }
            List<Route> routesAndTrains = (List<Route>)await repository.Routes.GetAllAsync(el => el.Train);
            for(int i = 0; i < routesAndTrains.Count; i++)
            {
                resultList[i].TrainNumber = routesAndTrains[i].Train.Number;
            }
            return resultList;
        }

        /// <summary>
        /// Getting the full time of travel.
        /// </summary>
        /// <param name="t1">Time departure</param>
        /// <param name="t2">Time arriving</param>
        /// <returns></returns>
        private TimeSpan GetTravelTime(DateTime t1, DateTime t2)
        {
            if (t1 < t2)
                return t2 - t1;
            else
                return t1 - t2;
        }
    }
}

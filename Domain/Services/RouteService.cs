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
    public class RouteService : IRouteFactory, IRouteService, IModel<RouteModel>
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
        /// Updating information about places
        /// </summary>
        /// <param name="vagon"></param>
        /// <returns>vagon</returns>
        public Vagon UpdateVagon(Vagon vagon)
        {
            vagon.BusyPaces = vagon.Tickets.Count;
            vagon.EmptyPlaces = vagon.Places - vagon.BusyPaces;
            return vagon;
        }

        /// <summary>
        /// Getting full information about route.
        /// </summary>
        /// <param name="id">route id.</param>
        /// <returns>Object whick includes all information about route.</returns>
        public RouteInfo GetRoute(int? id)
        {
            Route routeStations = repository.Routes.Find(r => r.Stations, r => r.Id == id).FirstOrDefault();
            Route routeTrain = repository.Routes.Find(r => r.Train, r => r.Id == id).FirstOrDefault();
            RouteInfo result = new RouteInfo
            {
                Id = routeStations.Id,
                Stations = routeStations.Stations,
                Train = routeTrain.Train,
                TravelTime = GetTravelTime(routeTrain.Stations[0].DepartureTime, 
                    routeTrain.Stations[routeTrain.Stations.Count - 1].ArrivingTime),
                Vagons = GetVagons(routeTrain.Train.Id)
            };
            result.EmptyPlaces = 0;
            foreach(var el in result.Vagons)
            {
                el.EmptyPlaces = el.Places - el.BusyPaces;
                result.EmptyPlaces += el.EmptyPlaces;
            }
            return result;
        }


        /// <summary>
        /// Getting the count vagons on train.
        /// </summary>
        /// <param name="id">Train id</param>
        /// <returns>The list of vagons.</returns>
        private List<Vagon> GetVagons(int id)
        {
            Train train = repository.Trains.Find(t => t.Vagons, t => t.Id == id).FirstOrDefault();
            return (List<Vagon>)train.Vagons;
        }

        /// <summary>
        /// Creates the list of RouteViewModels
        /// </summary>
        /// <returns>The list of RouteViewModels</returns>
        public async Task<List<RouteViewModel>> GetRoutes()
        {
            List<RouteViewModel> resultList = new List<RouteViewModel>();
            List<Route> routesAndStations = (List<Route>)await repository.Routes.GetAllAsync(el => el.Stations);
            foreach(var el in routesAndStations)
            {
                resultList.Add(new RouteViewModel
                {
                    Id = el.Id,
                    Stations = el.Stations,
                    IsRemoved = el.IsRemoved,
                    StartStation = el.Stations[0].Name,
                    StartDate = el.Stations[0].DepartureTime,
                    FinishDate = el.Stations[el.Stations.Count - 1].ArrivingTime,
                    FinishStation = el.Stations[el.Stations.Count - 1].Name,
                    TravelTime = GetTravelTime(el.Stations[0].DepartureTime, el.Stations[el.Stations.Count - 1].ArrivingTime)
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
        /// <returns>Travel time</returns>
        private TimeSpan GetTravelTime(DateTime t1, DateTime t2)
        {
            if (t1 < t2)
                return t2 - t1;
            else
                return t1 - t2;
        }

        /// <summary>
        /// Searching routes
        /// </summary>
        /// <param name="s1">the first station</param>
        /// <param name="s2">the last station</param>
        /// <returns>The list of Routes which inlsudes s1 and s2.</returns>
        public async Task<IEnumerable<RouteViewModel>> Search(List<RouteViewModel> list, string s1, string s2)
        {
            List<RouteViewModel> result = await GetRoutes();
            var selected = from el in result
                           where el.Stations.Find(s => s.Name == s1) != null
                            && el.Stations.Find(s => s.Name == s2) != null
                           select el;
            return selected;
        }

        /// <summary>
        /// Getting model with all properties.
        /// </summary>
        /// <param name="id">model id</param>
        /// <returns>model</returns>
        public RouteModel GetModel(int id)
        {
            Route routeS = repository.Routes.Find(r => r.Stations, r => r.Id == id).FirstOrDefault();
            Route routeT = repository.Routes.Find(r => r.Train, r => r.Id == id).FirstOrDefault();
            Train train = repository.Trains.Find(t => t.Vagons, t => t.Id == routeT.Train.Id).FirstOrDefault();
            RouteModel model = new RouteModel { Id = routeT.Id, Stations = routeS.Stations, Train = train };
            return model;
        }
    }
}

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
    public class TicketService : ITicketFactory
    {
        private readonly IUnitOfWork repository;

        public TicketService(IUnitOfWork unit)
        {
            if(unit != null)
            {
                repository = unit;
            }
            else
            {
                throw new NullReferenceException();
            }
        }


        /// <summary>
        /// Getting tickets which was bought.
        /// </summary>
        /// <returns>The list of bought tickets</returns>
        public async Task<IEnumerable<BuyTicketViewModel>> GetListBoughtTickets()
        {
            var list = await repository.ChosenTickets.GetAllAsync();
            List<BuyTicketViewModel> result = new List<BuyTicketViewModel>();
            foreach(var el in list)
            {
                Vagon vagon = await repository.Vagons.GetAsync(el.VagonId);
                Train train = await repository.Trains.GetAsync(el.TrainId);
                Route route = repository.Routes.Find(r => r.Stations, r => r.Id == el.RouteId).FirstOrDefault();
                result.Add(new BuyTicketViewModel
                {
                    ChosenTicketId = el.Id,
                    VagonNumber = vagon.Number,
                    VagonType = vagon.Type,
                    TrainNumber = train.Number,
                    Email = el.UserEmail,
                    RouteName = route.Stations[0].Name + " - " + route.Stations[route.Stations.Count - 1].Name
                });
            }
            return result;
        }
    }
}

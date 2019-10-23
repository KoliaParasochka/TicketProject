using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using ProjectDb.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TicketProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private EFUnitOfWork repository;
        private  RouteService routeService;

        public UserController()
        {
            repository = new EFUnitOfWork();
            routeService = new RouteService(repository);
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            var tickets = await repository.Tickets.GetAllAsync();
            var chosenTickets = tickets.Where(t => t.Email == User.Identity.Name);
            
            return View(chosenTickets);
        }

        [HttpPost]
        public async Task<ActionResult> BuyTicket(int routeId, int vagonId)
        {
            RouteInfo model = routeService.GetRoute(routeId);
            ChosenTicket chosenTicket = new ChosenTicket {
                VagonId = vagonId,
                RouteId = model.Id,
                TrainId = model.Train.Id,
                UserEmail = User.Identity.Name
            };
            await repository.ChosenTickets.CreateAsync(chosenTicket);
            return View();
        }

    }
}
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
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
    /// <summary>
    /// This class works wid admin's functions
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly TicketService ticketService;
        private readonly RouteService routeService;
        private readonly IUnitOfWork repository;

        public AdminController()
        {
            repository = new EFUnitOfWork();
            ticketService = new TicketService(repository);
            routeService = new RouteService(repository);
        }

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            var Tickets = await ticketService.GetListBoughtTickets();
            return View(Tickets);
        }

        /// <summary>
        /// Selling ticket.
        /// </summary>
        /// <param name="id">ticket id</param>
        /// <returns>Redirection to index page</returns>
        public async Task<ActionResult> Agree(int id)
        {
            var chosenTicket = await repository.ChosenTickets.GetAsync(id);
            IEnumerable<BuyTicketViewModel> tickets = await ticketService.GetListBoughtTickets();

            var ticket = tickets.Where(t => t.ChosenTicketId == id).FirstOrDefault();
            Vagon vagon = repository.Vagons.Find(v => v.Tickets, v => v.Id == chosenTicket.VagonId).FirstOrDefault();
            Ticket item = new Ticket
            {
                VagonId = chosenTicket.VagonId,
                VagonNumber = ticket.VagonNumber,
                Place = vagon.Tickets.Count + 1,
                TrainNumber = ticket.TrainNumber,
                VagonType = ticket.VagonType,
                RouteName = ticket.RouteName,
                Email = chosenTicket.UserEmail
            };
            
            
            if (vagon.Places >= vagon.Tickets.Count)
            {
                vagon.Tickets.Add(item);
                vagon = routeService.UpdateVagon(vagon);
                await repository.Vagons.UpdateAsync(vagon);
                
            }
            await repository.ChosenTickets.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Desagree from selling ticket.
        /// </summary>
        /// <param name="id">ticket id</param>
        /// <returns>Redirection to index page</returns>
        public async Task<ActionResult> Disagree(int id)
        {
            await repository.ChosenTickets.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
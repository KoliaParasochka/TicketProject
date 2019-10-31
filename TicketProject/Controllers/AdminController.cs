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
            ViewBag.Routes = await routeService.GetRoutes();
            return View(Tickets);
        }

        public async Task<ActionResult> Create(int? trainId, int? routeId)
        {
            if(routeId != null)
            {
                if(trainId != null)
                {
                    Train train = repository.Trains.Find(t => t.Vagons, t => t.Id == trainId).FirstOrDefault();
                    ViewBag.Train = train;
                }
                else
                {
                    ViewBag.Train = null;
                    ViewBag.MessageT = "Добавьте поезд!";
                }
                Route routeS = repository.Routes.Find(r => r.Stations, r => r.Id == routeId).FirstOrDefault();
                if (routeS.Stations != null)
                {
                    ViewBag.Stations = routeS.Stations;
                    if(routeS.Stations.Count < 2)
                        ViewBag.MessageS = "Добавьте минимум две станции!";
                }
                else
                {
                    ViewBag.Stations = null;
                    ViewBag.MessageS = "Добавьте минимум две станции!";
                }
                    
                return View(routeS);
            }
            Route route = new Route();
            await repository.Routes.CreateAsync(route);
            ViewBag.Stations = null;
            ViewBag.Message = "Добавьте поезд и минимум две станции!";
            return View(route);
        }

        [HttpGet]
        public ActionResult CreateStation(int routeId, int? trainId)
        {
            ViewBag.RouteId = routeId;
            ViewBag.TrainId = trainId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateStation(Station station, int idR, int? idT)
        {
            if (ModelState.IsValid)
            {
                Route route = repository.Routes.Find(r => r.Stations, r => r.Id == idR).FirstOrDefault();
                route.Stations.Add(station);
                if (await repository.Routes.UpdateAsync(route))
                    ViewBag.Stations = route.Stations;
                else
                    ViewBag.Stations = null;
                ViewBag.RouteId = idR;
                ViewBag.TrainId = idT;
                return RedirectToAction("Create", new { trainId = idT, routeId = idR });
            }
            return View(station);
        }

        [HttpGet]
        public ActionResult CreateTrain(int? trainId, int? routeId)
        {
            if(trainId != null)
            {
                Train train = repository.Trains.Find(t => t.Vagons, t => t.Id == trainId).FirstOrDefault();
                ViewBag.Vagons = train.Vagons;
                ViewBag.Train = train;
                ViewBag.TrainId = trainId;
                if(train.Vagons.Count < 1)
                {
                    ViewBag.Message = "Добавьте хотя бы один вагон!";
                }
            }
            else
            {
                ViewBag.Vagons = null;
                ViewBag.Train = null;
                ViewBag.Message = "Добавьте хотя бы один вагон!";
            }
            

            ViewBag.RouteId = routeId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateTrain(Train train, int id)
        {
            if (ModelState.IsValid)
            {
                Route route = repository.Routes.Find(r => r.Train, r => r.Id == id).FirstOrDefault();
                route.Train = train;
                if (await repository.Routes.UpdateAsync(route))
                    ViewBag.Train = train;
                else
                    ViewBag.Train = null;
                ViewBag.TrainId = train.Id;
                ViewBag.RouteId = id;
                return RedirectToAction("CreateTrain", new { trainId = train.Id, routeId = id });
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult CreateVagon(int idT, int idR)
        {
            ViewBag.TrainId = idT;
            ViewBag.RouteId = idR;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateVagon(Vagon vagon, int idT, int idR)
        {
            if (ModelState.IsValid)
            {
                Train train = repository.Trains.Find(t => t.Vagons, t => t.Id == idT).FirstOrDefault();
                train.Vagons.Add(vagon);
                if (await repository.Trains.UpdateAsync(train))
                    ViewBag.Vagons = train.Vagons;
                else
                    ViewBag.Vagons = null;
                return RedirectToAction("CreateTrain", new { trainId = idT, routeId = idR });
            }
            return View(vagon);
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
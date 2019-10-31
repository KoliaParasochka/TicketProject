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

        /// <summary>
        /// Changing route
        /// </summary>
        /// <param name="id">route id</param>
        /// <returns>View</returns>
        public ActionResult Change(int id, string message)
        {
            RouteModel model = routeService.GetModel(id);
            ViewBag.Message = message;
            return View(model);
        }

        /// <summary>
        /// Removing station
        /// </summary>
        /// <param name="idS">station id</param>
        /// <param name="idR">route id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteStation(int idS, int idR)
        {
            await repository.Stations.DeleteAsync(idS);
            return RedirectToAction("Change", new { id = idR });
        }

        /// <summary>
        /// Getting station from database
        /// </summary>
        /// <param name="idS">station id</param>
        /// <param name="idR">route id</param>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<ActionResult> ChangeStation(int idS, int idR)
        {
            Station station = await repository.Stations.GetAsync(idS);
            ViewBag.idR = idR;
            return View(station);
        }

        /// <summary>
        /// Putting updated station into database
        /// </summary>
        /// <param name="idS">station id</param>
        /// <param name="idR">route id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ChangeStation(Station model, int idR)
        {
            model.RouteId = idR;
            if (ModelState.IsValid)
            {
                await repository.Stations.UpdateAsync(model);
                return RedirectToAction("Change", new { id = idR });
            }
            return View(model);
        }

        /// <summary>
        /// Adding new Station to route
        /// </summary>
        /// <param name="idR">route id</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult AddStation(int idR)
        {
            ViewBag.idR = idR;
            return View();
        }

        /// <summary>
        /// Adding new Station to route
        /// </summary>
        /// <param name="idR">route id</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<ActionResult> AddStation(Station model, int idR)
        {
            model.RouteId = idR;
            if (ModelState.IsValid)
            {
                await repository.Stations.CreateAsync(model);
                return RedirectToAction("Change", new { id = idR });
            }
            return View(model);
        }

        /// <summary>
        /// Getting train from database to change it.
        /// </summary>
        /// <param name="idT"></param>
        /// <param name="idR"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangeTrain(int idT, int idR)
        {
            ViewBag.idR = idR;
            ViewBag.idT = idT;
            Train train = repository.Trains.Find(t => t.Vagons, t => t.Id == idT).FirstOrDefault();
            return View(train);
        }

        /// <summary>
        /// Changing train
        /// </summary>
        /// <param name="model">train</param>
        /// <param name="idR">route id</param>
        /// <returns>Resirection or view (it depents on validating of the data)</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeTrain(Train model, int idR)
        {
            if (ModelState.IsValid)
            {
                await repository.Trains.UpdateAsync(model);
                return RedirectToAction("Change", new { id = idR });
            }
            return View(model);
        }

        /// <summary>
        /// Removing vagon
        /// </summary>
        /// <param name="idV">vagon id</param>
        /// <param name="idR">route id</param>
        /// <returns>Redirection</returns>
        [HttpPost]
        public async Task<ActionResult> DeleteVagon(int idV, int idR)
        {
            Ticket ticket = repository.Tickets.Find(t => t.Vagon, t => t.VagonId == idV).FirstOrDefault();
            if (ticket == null)
            {
                await repository.Vagons.DeleteAsync(idV);
                return RedirectToAction("Change", new { id = idR });
            }

            return RedirectToAction("Change", new { id = idR,
                message = "Невозможно удалить вагон, на который уже куплены билеты" });
        }

        /// <summary>
        /// Adding new vagon, but this mathod takes it from database 
        /// </summary>
        /// <param name="idR">route id</param>
        /// <param name="idT">train id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddVagon(int idR, int idT)
        {
            ViewBag.idR = idR;
            ViewBag.idT = idT;
            return View();
        }

        /// <summary>
        /// Putting updeted data into database
        /// </summary>
        /// <param name="model">updated model</param>
        /// <param name="idR">route id</param>
        /// <param name="idT">train id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddVagon(Vagon model, int idR, int idT)
        {
            model.TrainId = idT;
            if (ModelState.IsValid)
            {
                await repository.Vagons.CreateAsync(model);
                return RedirectToAction("Change", new { id = idR });
            }
            return View(model);
        }

        /// <summary>
        /// Taking vagon from database.
        /// </summary>
        /// <param name="idV">vagon id</param>
        /// <param name="idR">route id</param>
        /// <param name="idT">train id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangeVagon(int idV, int idR, int idT)
        {
            ViewBag.idR = idR;
            ViewBag.idV = idV;
            ViewBag.idT = idT;
            Vagon vagon = repository.Vagons.Find(v => v.Tickets, v => v.Id == idV).FirstOrDefault();
            if(vagon.Tickets.Count == 0)
                return View(vagon);
            return RedirectToAction("Change", new { id = idR,
                message = "Невозможно поменять вагон, на который уже куплены билеты!" });
        }

        /// <summary>
        /// Putting updated data into database.
        /// </summary>
        /// <param name="model">updated vagon</param>
        /// <param name="idR">route id</param>
        /// <param name="idT">train id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ChangeVagon(Vagon model, int idR, int idT)
        {
            model.TrainId = idT;
            if (ModelState.IsValid)
            {
                await repository.Vagons.UpdateAsync(model);
                return RedirectToAction("Change", new { id = idR });
            }
            return View(model);
        }

        /// <summary>
        /// Creating route
        /// </summary>
        /// <param name="trainId"></param>
        /// <param name="routeId"></param>
        /// <returns>View</returns>
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

        /// <summary>
        /// Creating station for route
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="trainId"></param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateStation(int routeId, int? trainId)
        {
            ViewBag.RouteId = routeId;
            ViewBag.TrainId = trainId;
            return View();
        }


        /// <summary>
        /// Creating station (getting input data from view)
        /// </summary>
        /// <param name="station"></param>
        /// <param name="idR"></param>
        /// <param name="idT"></param>
        /// <returns>View</returns>
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


        /// <summary>
        /// Creating train
        /// </summary>
        /// <param name="trainId"></param>
        /// <param name="routeId"></param>
        /// <returns>View</returns>
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

        /// <summary>
        /// Creating train (getting input data from model)
        /// </summary>
        /// <param name="train"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creating vagon
        /// </summary>
        /// <param name="idT">train id</param>
        /// <param name="idR">route id</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult CreateVagon(int idT, int idR)
        {
            ViewBag.TrainId = idT;
            ViewBag.RouteId = idR;
            return View();
        }

        /// <summary>
        /// Creating vagon (getting input data from view)
        /// </summary>
        /// <param name="vagon"></param>
        /// <param name="idT">train id</param>
        /// <param name="idR">route id</param>
        /// <returns>View</returns>
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
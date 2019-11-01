using Domain.Entities;
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
    /// This is a main controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly EFUnitOfWork repository;
        private readonly RouteService routeService;

        public HomeController()
        {
            repository = new EFUnitOfWork();
            routeService = new RouteService(repository);
        }

        /// <summary>
        /// Action for Main gape
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var list = await routeService.GetRoutes();
            return View(list);
        }

        /// <summary>
        /// Action for about page
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Show us all routes
        /// </summary>
        /// <param name="id">route id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowRoute(int? id)
        {
            RouteInfo route = routeService.GetRoute(id);
            return View(route);
        }

        [HttpPost]
        public async Task<ActionResult> Index(string firstStation, string finishStation)
        {
            var routes = await routeService.GetRoutes();
            var list = await routeService.Search(routes, firstStation, finishStation);
            return View(list);
        }
    }
}
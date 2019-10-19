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
    public class HomeController : Controller
    {
        private readonly EFUnitOfWork repository;
        private readonly RouteService routeService;

        public HomeController()
        {
            repository = new EFUnitOfWork();
            routeService = new RouteService(repository);
        }


        public async Task<ActionResult> Index()
        {
            ViewBag.Routes = await routeService.GetRoutes();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult ShowRoute(int id)
        {
            RouteInfo route = routeService.GetRoute(id);
            return View(route);
        }

        [HttpPost]
        public ActionResult BuyTicket(RouteInfo route)
        {
            
            return View(route);
        }
    }
}
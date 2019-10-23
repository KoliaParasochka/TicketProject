using Domain.Entities;
using Domain.Interfaces;
using ProjectDb.EF;
using ProjectDb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDb.Storage
{
    /// <summary>
    /// This is a class which creates access 
    /// to any property in database context
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db;
        private IRepository<Route> routeRepository; 
        private IRepository<Station> stationRepository; 
        private IRepository<Train> trainRepository; 
        private IRepository<Vagon> vagonRepository;
        private IRepository<Ticket> ticketRepository;
        private IRepository<MyUser> userRepository;
        private IRepository<ChosenTicket> chosenticketRepository;

        public EFUnitOfWork()
        {
            db = new ApplicationDbContext();
        }

        /// <summary>
        /// Getting the onject of RouteRepository
        /// </summary>
        public IRepository<Route> Routes {
            get
            {
                if (routeRepository == null)
                    routeRepository = new RouteRepository(db);
                return routeRepository;
            }
        }

        /// <summary>
        /// Getting the onject of StationRepository
        /// </summary>
        public IRepository<Station> Stations
        {
            get
            {
                if (stationRepository == null)
                    stationRepository = new StationRepository(db);
                return stationRepository;
            }
        }

        /// <summary>
        /// Getting the onject of TarinRepository
        /// </summary>
        public IRepository<Train> Trains
        {
            get
            {
                if (trainRepository == null)
                    trainRepository = new TrainRepository(db);
                return trainRepository;
            }
        }

        /// <summary>
        /// Getting the onject of TicketRepository
        /// </summary>
        public IRepository<Ticket> Tickets
        {
            get
            {
                if (ticketRepository == null)
                    ticketRepository = new TicketRepository(db);
                return ticketRepository;
            }
        }

        /// <summary>
        /// Getting the onject of MyUserRepository
        /// </summary>
        public IRepository<MyUser> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        /// <summary>
        /// Getting the onject of VagonRepository
        /// </summary>
        public IRepository<Vagon> Vagons
        {
            get
            {
                if (vagonRepository == null)
                    vagonRepository = new VagonRepository(db);
                return vagonRepository;
            }
        }

        /// <summary>
        /// Getting the onject of BuyTicketRepository
        /// </summary>
        public IRepository<ChosenTicket> ChosenTickets
        {
            get
            {
                if (chosenticketRepository == null)
                    chosenticketRepository = new BuyTicket(db);
                return chosenticketRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        db.Dispose();
                    }
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

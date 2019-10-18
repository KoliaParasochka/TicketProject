using Domain.Entities;
using System;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Route> Routes { get; }
        IRepository<Station> Stations { get; }
        IRepository<Train> Trains { get; }
        IRepository<Vagon> Vagons { get; }
        IRepository<Ticket> Tickets { get; }
        IRepository<MyUser> Users { get; }
    }
}

using Domain.Entities;
using System;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Route> Routes { get; set; }
        IRepository<Station> Stations { get; set; }
        IRepository<Train> Trains { get; set; }
        IRepository<Vagon> Vagons { get; set; }
    }
}

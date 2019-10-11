using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDb.EF
{
    /// <summary>
    /// This is a database context which. 
    /// The objects of this class give access to database
    /// </summary>

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Station> Stations { get; set; }    // The list of stations.
        public DbSet<Route> Routes { get; set; }        // The list of routes.
        public DbSet<Train> Trains { get; set; }        // The list of trains.
        public DbSet<Vagon> Vagons { get; set; }        // The list of vagons.

        /// <summary>
        /// Setting connection string as 'IdentityDb'
        /// </summary>
        public ApplicationDbContext() : base("IdentityDb")
        {
        }

        /// <summary>
        /// Creating the object of ApplicationDbContext.
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

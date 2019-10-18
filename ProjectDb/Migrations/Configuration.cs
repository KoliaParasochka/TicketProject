namespace ProjectDb.Migrations
{
    using Domain.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ProjectDb.EF;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectDb.EF.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectDb.EF.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var userManager = new AppUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "kolian@mail.ru", UserName = "kolian@mail.ru" };
            string password = "Zakashmar_99";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            Station s4 = new Station { Name = "Запорожье", ArrivingTime = new DateTime(2019, 11, 1, 14, 15, 0), DepartureTime = new DateTime(2019, 11, 1, 15, 30, 0) };
            Station s1 = new Station { Name = "Харьков", ArrivingTime = new DateTime(2019, 11, 1, 8, 15, 0), DepartureTime = new DateTime(2019, 11, 1, 9, 30, 0) };
            Station s3 = new Station { Name = "Павлоград", ArrivingTime = new DateTime(2019, 11, 1, 14, 15, 0), DepartureTime = new DateTime(2019, 11, 1, 14, 20, 0) };
            Station s2 = new Station { Name = "Красноград", ArrivingTime = new DateTime(2019, 11, 1, 10, 25, 0), DepartureTime = new DateTime(2019, 11, 1, 10, 30, 0) };
            Station s5 = new Station { Name = "Одесса", ArrivingTime = new DateTime(2019, 11, 11, 10, 15, 0), DepartureTime = new DateTime(2019, 11, 11, 11, 30, 0) };
            Station s7 = new Station { Name = "Николаев", ArrivingTime = new DateTime(2019, 12, 8, 18, 15, 0), DepartureTime = new DateTime(2019, 12, 8, 18, 30, 0) };
            Station s6 = new Station { Name = "Херсон", ArrivingTime = new DateTime(2019, 12, 8, 15, 15, 0), DepartureTime = new DateTime(2019, 12, 8, 16, 30, 0) };
            Station s8 = new Station { Name = "Хмельницкий", ArrivingTime = new DateTime(2019, 12, 9, 9, 15, 0), DepartureTime = new DateTime(2019, 12, 9, 10, 30, 0) };
            Station s9 = new Station { Name = "Хмельницкий", ArrivingTime = new DateTime(2019, 11, 11, 19, 15, 0), DepartureTime = new DateTime(2019, 11, 11, 19, 30, 0) };
            Station s10 = new Station { Name = "Львов", ArrivingTime = new DateTime(2019, 11, 12, 14, 55, 0), DepartureTime = new DateTime(2019, 11, 12, 16, 0, 0) };

            context.Stations.AddRange(new List<Station> { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10 });
            context.SaveChanges();

            Route r1 = new Route { Stations = new List<Station>() };
            r1.Stations.AddRange(new List<Station> { s1, s2, s3, s4 });

            Route r2 = new Route { Stations = new List<Station>() };
            r2.Stations.AddRange(new List<Station> { s6, s7, s8 });

            Route r3 = new Route { Stations = new List<Station>() };
            r3.Stations.AddRange(new List<Station> { s5, s9, s10 });

            context.Routes.AddRange(new List<Route> { r1, r2, r3 });
            context.SaveChanges();

            Train t1 = new Train { Id = r1.Id, Number = 231, Price = 120m, Route = r1 };
            Train t2 = new Train { Id = r2.Id, Number = 501, Price = 90m, Route = r2 };
            Train t3 = new Train { Id = r3.Id, Number = 873, Price = 115m, Route = r3 };

            context.Trains.AddRange(new List<Train> { t1, t2, t3 });
            context.SaveChanges();

            Vagon v1 = new Vagon { Number = 1, Places = 100, Type = "Плацкарт", Train = t1, BusyPaces = 100 };
            Vagon v2 = new Vagon { Number = 2, Places = 80, Type = "Купе", Train = t1, BusyPaces = 57 };
            Vagon v3 = new Vagon { Number = 3, Places = 100, Type = "Плацкарт", Train = t1 };
            Vagon v4 = new Vagon { Number = 1, Places = 90, Type = "Общий", Train = t2 };
            Vagon v5 = new Vagon { Number = 2, Places = 70, Type = "Купе", Train = t2, BusyPaces = 70 };
            Vagon v6 = new Vagon { Number = 3, Places = 76, Type = "Купе", Train = t2 };
            Vagon v7 = new Vagon { Number = 1, Places = 87, Type = "Общий", Train = t3, BusyPaces = 50 };
            Vagon v8 = new Vagon { Number = 2, Places = 100, Type = "Плацкарт", Train = t3 };

            context.Vagons.AddRange(new List<Vagon> { v1, v2, v3, v4, v5, v6, v7, v8, });
            context.SaveChanges();
        }
    }
}

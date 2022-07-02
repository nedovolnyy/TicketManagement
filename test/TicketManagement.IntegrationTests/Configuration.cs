using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.DI;
using TicketManagement.DataAccess.EF;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class Configuration
    {
        public Configuration()
        {
            Container = new Container();

            Setup();
        }

        public static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public Container Container { get; }

        public void Setup()
        {
            Container.Register<IAreaService>(() =>
            {
                return new AreaService(Container.GetInstance<IAreaRepository>());
            }, Lifestyle.Singleton);

            Container.Register<IEventAreaService>(() =>
            {
                return new EventAreaService(Container.GetInstance<IEventAreaRepository>());
            }, Lifestyle.Singleton);

            Container.Register<IEventSeatService>(() =>
            {
                return new EventSeatService(Container.GetInstance<IEventSeatRepository>());
            }, Lifestyle.Singleton);

            Container.Register<IEventService>(() =>
            {
                var options = new EventRepository(Container.GetInstance<IDatabaseContext>());
                return new EventService(options);
            }, Lifestyle.Singleton);

            Container.Register<ILayoutService>(() =>
            {
                return new LayoutService(Container.GetInstance<ILayoutRepository>());
            }, Lifestyle.Singleton);

            Container.Register<ISeatService>(() =>
            {
                return new SeatService(Container.GetInstance<ISeatRepository>());
            }, Lifestyle.Singleton);

            Container.Register<IVenueService>(() =>
            {
                return new VenueService(Container.GetInstance<IVenueRepository>());
            }, Lifestyle.Singleton);

            Container.Register<IDatabaseContext>(() =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                return new DatabaseContext(optionsBuilder.UseSqlServer(ConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options);
            }, Lifestyle.Transient);

            Container.Register<IAreaRepository>(() =>
            {
                return new AreaRepository(Container.GetInstance<IDatabaseContext>());
            }, Lifestyle.Transient);

            Container.Register<IEventAreaRepository>(() =>
            {
                return new EventAreaRepository(Container.GetInstance<IDatabaseContext>());
            }, Lifestyle.Transient);

            Container.Register<IEventSeatRepository>(() =>
            {
                return new EventSeatRepository(Container.GetInstance<IDatabaseContext>());
            }, Lifestyle.Transient);

            Container.Register<IEventRepository>(() =>
            {
                return new EventRepository(Container.GetInstance<IDatabaseContext>());
            }, Lifestyle.Transient);

            Container.Register<ILayoutRepository>(() =>
            {
                return new LayoutRepository(Container.GetInstance<IDatabaseContext>());
            }, Lifestyle.Transient);

            Container.Register<ISeatRepository>(() =>
            {
                return new SeatRepository(Container.GetInstance<IDatabaseContext>());
            }, Lifestyle.Transient);

            Container.Register<IVenueRepository>(() =>
            {
                return new VenueRepository(Container.GetInstance<IDatabaseContext>());
            }, Lifestyle.Transient);
        }
    }
}
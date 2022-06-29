using System.Configuration;
using SimpleInjector;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.DI;
using TicketManagement.DataAccess.EF;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
#pragma warning disable S1200 // Classes should not be coupled to too many other classes (Single Responsibility Principle)
    public class Configuration
#pragma warning restore S1200 // Classes should not be coupled to too many other classes (Single Responsibility Principle)
    {
        private readonly IDatabaseContext _databaseContext = new DatabaseContext(ConnectionString);
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
                var options = new AreaRepository(_databaseContext);
                return new AreaService(options);
            }, Lifestyle.Singleton);

            Container.Register<IEventAreaService>(() =>
            {
                var options = new EventAreaRepository(_databaseContext);
                return new EventAreaService(options);
            }, Lifestyle.Singleton);

            Container.Register<IEventSeatService>(() =>
            {
                var options = new EventSeatRepository(_databaseContext);
                return new EventSeatService(options);
            }, Lifestyle.Singleton);

            Container.Register<IEventService>(() =>
            {
                var options = new EventRepository(_databaseContext);
                return new EventService(options);
            }, Lifestyle.Singleton);

            Container.Register<ILayoutService>(() =>
            {
                var options = new LayoutRepository(_databaseContext);
                return new LayoutService(options);
            }, Lifestyle.Singleton);

            Container.Register<ISeatService>(() =>
            {
                var options = new SeatRepository(_databaseContext);
                return new SeatService(options);
            }, Lifestyle.Singleton);

            Container.Register<IVenueService>(() =>
            {
                var options = new VenueRepository(_databaseContext);
                return new VenueService(options);
            }, Lifestyle.Singleton);

            Container.Register<IDatabaseContext>(() =>
            {
                return _databaseContext;
            }, Lifestyle.Transient);

            Container.Register<IAreaRepository>(() =>
            {
                return new AreaRepository(_databaseContext);
            }, Lifestyle.Transient);

            Container.Register<IEventAreaRepository>(() =>
            {
                return new EventAreaRepository(_databaseContext);
            }, Lifestyle.Transient);

            Container.Register<IEventSeatRepository>(() =>
            {
                return new EventSeatRepository(_databaseContext);
            }, Lifestyle.Transient);

            Container.Register<IEventRepository>(() =>
            {
                return new EventRepository(_databaseContext);
            }, Lifestyle.Transient);

            Container.Register<ILayoutRepository>(() =>
            {
                return new LayoutRepository(_databaseContext);
            }, Lifestyle.Transient);

            Container.Register<ISeatRepository>(() =>
            {
                return new SeatRepository(_databaseContext);
            }, Lifestyle.Transient);

            Container.Register<IVenueRepository>(() =>
            {
                return new VenueRepository(_databaseContext);
            }, Lifestyle.Transient);
        }
    }
}
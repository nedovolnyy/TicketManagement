using System.Reflection;
using System.Web.Mvc;
using log4net;
using log4net.Core;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using ThirdPartyEventEditor.Repository;

namespace ThirdPartyEventEditor
{
    public static class DIConfiguration
    {
        public static void ConfigureInjector()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<JsonRepository>(Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.RegisterConditional(typeof(ILog), c => typeof(Log4NetAdapter<>).MakeGenericType(
                c.Consumer.ImplementationType), Lifestyle.Singleton, c => true);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }

    public sealed class Log4NetAdapter<T> : LogImpl
    {
        public Log4NetAdapter()
            : base(LogManager.GetLogger(typeof(T)).Logger)
        {
        }
    }
}
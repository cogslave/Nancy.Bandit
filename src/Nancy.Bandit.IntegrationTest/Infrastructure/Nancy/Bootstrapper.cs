
namespace Nancy.Bandit.IntegrationTest.Infrastructure
{
    using Nancy.Bandit.Selection;
    using Nancy.Bandit.Session;
    using Nancy.Bootstrapper;
    using Nancy.Session;
    using Nancy.TinyIoc;
    using System.Collections.Generic;
    using System.Configuration;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            CookieBasedSessions.Enable(pipelines);
            
            pipelines.BeforeRequest += (ctx) =>
            {
                ((Bandido)container.Resolve<IBandit>()).Session = new NancySessionAdapter(ctx.Request.Session);
                return null;
            };
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            IBandit bandit = new Bandido(new MemoryRepository());
            container.Register<IBandit>(bandit);
        }
        
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
        }
    }
}
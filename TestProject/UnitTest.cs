using CrudFortesCore.Data;
using CrudFortesCore.Repository.Abstract;
using CrudFortesCore.Repository.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TestProject
{
    public abstract class UnitTest
    {
        protected static Container container;
        protected Scope containerScope;

        protected IContext context;

        [SetUp]
        public virtual void InitTeste()
        {
            container = new Container();
            containerScope = AsyncScopedLifestyle.BeginScope(container);

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);
            container.Options.AllowOverridingRegistrations = true;
            container.Options.ResolveUnregisteredConcreteTypes = true;
            var assemblies = GetAssemblies().ToArray();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IRequestHandler<>), assemblies);
            container.Collection.Register(typeof(INotificationHandler<>), assemblies);
            container.Collection.Register(typeof(IPipelineBehavior<,>), assemblies);
            container.RegisterInstance(new ServiceFactory(container.GetInstance));

            var optionsBuilder = new DbContextOptionsBuilder<Context>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CrudFortesCoreContextTest;Trusted_Connection=True;MultipleActiveResultSets=true");

            container.Register<IContext>(() => new Context(optionsBuilder.Options));

            context = container.GetInstance<IContext>();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.BeginTransaction();
        }

        [TearDown]
        public virtual void TestDown()
        {
            context?.Database.RollbackTransaction();
            containerScope.Dispose();
            container.Dispose();
            context.Database.EnsureDeleted();
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return typeof(Repository<>).GetTypeInfo().Assembly;
        }
    }
}

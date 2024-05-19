using D1TechTestCase.Core.Repositories;
using D1TechTestCase.Core.Services;
using D1TechTestCase.Core.UnitOfWorks;
using D1TechTestCase.Repository.Repositories;
using D1TechTestCase.Repository.UnitOfWorks;
using D1TechTestCase.Repository;
using D1TechTestCase.Service.Mapping;
using D1TechTestCase.Service.Services;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace D1TechTestCase.API.RepoServiceModule
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();



            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();


            // builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();

        }
    }
}

using Autofac;
using EcommerceCore.Domain;

namespace EcommerceCore.Websites.Modules
{
    public class EFModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            //builder.RegisterType(typeof(EcommerceDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();          
            builder.RegisterType<EcommerceDbContext>().InstancePerLifetimeScope();

        }
    }
}
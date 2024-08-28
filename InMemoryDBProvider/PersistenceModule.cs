using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDBProvider
{
    public class PersistenceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(x =>
            {
                var configuration = x.Resolve<IConfiguration>();
                var cnnString = configuration?.GetConnectionString("WebApiDatabase");
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                var DbPath = System.IO.Path.Join(path, cnnString);
                var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
                optionsBuilder.UseSqlite($"Data Source={DbPath}", builder => builder.MigrationsAssembly("InMemoryDBProvider"));
                return new ProductContext(optionsBuilder.Options);

            }).InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>().AsSelf().InstancePerLifetimeScope();

        }
    }
}

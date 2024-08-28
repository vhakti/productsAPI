
namespace webAPI
{
   
    using Model;
    using Model.Ports;
    using Serilog;
    using Serilog.Sinks;
    using webAPI.Middlewares;
    using Services;
    using InMemoryDBProvider;
    using Autofac.Extensions.DependencyInjection;
    using Autofac;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Caching.Memory;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<ExternalOptions>(builder.Configuration.GetSection("External")); 

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
               .ConfigureContainer<ContainerBuilder>((container) =>
               {
                   container.RegisterModule<PersistenceModule>();
               });

            builder.Services.AddHttpClient<IExternalService, DiscountService>().ConfigureHttpClient(
              c => { c.BaseAddress = new Uri(builder.Configuration["External:mockapiUrl"]); c.Timeout = TimeSpan.FromSeconds(5); });

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddMemoryCache();
            var app = builder.Build();
            app.UseMiddleware<PerformanceMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          
            app.UseHttpsRedirection();

            app.UseAuthorization();
           
            app.MapControllers();

            AddCacheItems(app);

            app.Run();
            
           

        }

        private static void AddCacheItems(WebApplication app)
        {
            var cache = app.Services.GetRequiredService<IMemoryCache>();

       
            
            Dictionary<int, string> status_prod = new Dictionary<int, string>();
            status_prod.Add(0, "Inactive");
            status_prod.Add(1, "Active");
            cache.Set("status_product", status_prod, TimeSpan.FromMinutes(5));
        }
    }
}

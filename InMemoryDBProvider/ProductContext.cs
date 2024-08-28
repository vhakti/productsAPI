namespace InMemoryDBProvider
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using Model;
    using Microsoft.Extensions.Configuration;

    public class ProductContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        //public string DbPath { get; }
        //protected readonly IConfiguration Configuration;
        //public ProductContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public ProductContext() { }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

            //    var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = System.IO.Path.Join(path, "ProductCatalog.db");
           
         }

            //protected override void OnConfiguring(DbContextOptionsBuilder options)
            //=> options.UseSqlite($"Data Source={DbPath}");

            //protected override void OnConfiguring(DbContextOptionsBuilder options)
            //    {
            //        // connect to sqlite database
            //        options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
            //    }


        }
}

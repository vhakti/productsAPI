using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDBProvider
{
    public class MigrationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        private string DbPath { get; set; }

        public MigrationContext() { InitConnStr(); }
        public MigrationContext(DbContextOptions<MigrationContext> options) : base(options)
        {
            InitConnStr();


        }

        private void InitConnStr()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "ProductsCatalog.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite(
                   $"Data Source={DbPath}",           
                  x => x.MigrationsAssembly("InMemoryDBProvider"));
            }
            base.OnConfiguring(options);
        }
      
    }
}

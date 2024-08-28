using InMemoryDBProvider;
using Microsoft.EntityFrameworkCore;

namespace PersistenceTests
{
    public class SqLiteTests
    {
        string DbPath = @"ProductsCatalog";
        [Fact]
        public void ConnectToDB()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "ProductsCatalog.db");

            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
            optionsBuilder.UseSqlite(DbPath);

            ProductContext myContext = new ProductContext(optionsBuilder.Options);

            Assert.True(myContext.Database.CanConnect());

            myContext.Dispose();

        }
    }
}
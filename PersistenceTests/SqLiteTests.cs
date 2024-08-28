using InMemoryDBProvider;
using Microsoft.EntityFrameworkCore;
using Model;
using Moq;
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
            optionsBuilder.UseSqlite($"Data Source={DbPath}");

            ProductContext myContext = new ProductContext(optionsBuilder.Options);

            Assert.True(myContext.Database.CanConnect());

            myContext.Dispose();

        }

        [Fact]
        public async void create_product_repo_test()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "ProductsCatalog.db");

            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
            optionsBuilder.UseSqlite($"Data Source={DbPath}");

            ProductContext myContext = new ProductContext(optionsBuilder.Options);
            GenericRepo<Product> repo = new ProductRepository(myContext);

            //new
            var prod = Mock.Of<Product>();
            prod.Stock = 1;
            prod.Name = "tablet";
            prod.Status = 1;
            prod.Stock = 10;
            prod.Description = "ios tablet";
            prod.CreatedOn = DateTime.Now;

            var _product = await repo.Insert(prod);

            Assert.NotNull(_product);

            var getProduct = await repo.GetById((int)_product);

            Assert.NotNull(getProduct);
        }
    }
}
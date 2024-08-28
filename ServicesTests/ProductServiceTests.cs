namespace ServicesTests
{
    using InMemoryDBProvider;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Model;
    using Model.Ports;
    using Moq;
    using Services;

    public class ProductServiceTests
    {
        string DbPath = @"ProductsCatalog";

        [Fact]
        public async void create_valid_product()
        {
            //var mockProduct = new Product() { Name = "Test",Price=10,Description="My Product detail",Status=1,CreatedOn=DateTime.Now ,Stock=100};
            //IMemoryCache cache;
            //IProductService serv = new ProductService()
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "ProductsCatalog.db");

            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
            optionsBuilder.UseSqlite($"Data Source={DbPath}");

            ProductContext myContext = new ProductContext(optionsBuilder.Options);
            GenericRepo<Product> repo = new ProductRepository(myContext);
            var _product = await repo.GetById(1);

            var prod = new Mock<Product>();
            Assert.NotNull(prod.Object);



        }
        [Fact]
        public void cache_add_and_get_test()
        {
            var expectedKey = "expectedKey";
            var expectedValue = "expectedValue";
            var expectedMilliseconds = 100;
            var mockCache = new Mock<IMemoryCache>();
            var mockCacheEntry = new Mock<ICacheEntry>();

            string? keyPayload = null;
            mockCache
                .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                .Callback((object k) => keyPayload = (string)k)
                .Returns(mockCacheEntry.Object); // this should address your null reference exception

            object? valuePayload = null;
            mockCacheEntry
                .SetupSet(mce => mce.Value = It.IsAny<object>())
                .Callback<object>(v => valuePayload = v);

            TimeSpan? expirationPayload = null;
            mockCacheEntry
                .SetupSet(mce => mce.AbsoluteExpirationRelativeToNow = It.IsAny<TimeSpan?>())
                .Callback<TimeSpan?>(dto => expirationPayload = dto);

            // Act
            //var success = _target.SetCacheValue(expectedKey, expectedValue,
//new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMilliseconds(expectedMilliseconds)));

            // Assert
           //Assert.True(success);
            Assert.Equal(expectedKey, keyPayload);
            Assert.Equal(expectedValue, valuePayload as string);
            Assert.Equal(TimeSpan.FromMilliseconds(expectedMilliseconds), expirationPayload);
        }
    }
}
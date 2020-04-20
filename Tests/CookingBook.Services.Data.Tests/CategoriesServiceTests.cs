namespace CookingBook.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CookingBook.Data;
    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Data.Repositories;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Categories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Moq;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Category>>();
            repository.Setup(r => r.All()).Returns(new System.Collections.Generic.List<Category>
            {
                new Category(),
                new Category(),
                new Category(),
            }.AsQueryable());
            var service = new CategoriesService(repository.Object);

            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetAllShouldReturnIEnumerableAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);

            Assert.Equal(3, service.GetAll<CategoryViewModel>().Count());
            Assert.IsType<CategoryViewModel>(service.GetAll<CategoryViewModel>().FirstOrDefault());
        }

        [Fact]
        public void GetAllShouldReturnNothingWhenNoElementsAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);

            Assert.Empty(service.GetAll<CategoryViewModel>());
        }

        [Fact]
        public async Task GetByIdShouldReturnCategoryByGivenIdAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            dbContext.Add(new Category { Id = 1, Title = "firstCategory", Image = "firstImage" });
            dbContext.Add(new Category { Id = 2, Title = "secondCategory", Image = "secondImage" });
            dbContext.Add(new Category { Id = 3, Title = "thirdCategory", Image = "thirdImage" });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);

            var result = service.GetById<CategoryViewModel>(2);
            Assert.IsType<CategoryViewModel>(result);
            Assert.Equal("secondCategory", result.Title);
        }

        [Fact]
        public async Task DeleteByIdAsyncShouldDeleteTheProperEntity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            dbContext.Add(new Category { Id = 1, Title = "firstCategory", Image = "firstImage" });
            dbContext.Add(new Category { Id = 2, Title = "secondCategory", Image = "secondImage" });
            dbContext.Add(new Category { Id = 3, Title = "thirdCategory", Image = "thirdImage" });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);

            var result = service.DeleteById(3);
            Assert.DoesNotContain(new Category { Id = 3, Title = "thirdCategory", Image = "thirdImage" }, dbContext.Categories);
            await Assert.ThrowsAnyAsync<Exception>(() => service.DeleteById(234));
        }

        [Fact]
        public async Task EditByIdShouldChangeTheCategoryName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            dbContext.Add(new Category { Id = 1, Title = "firstCategory", Image = "firstImage" });
            dbContext.Add(new Category { Id = 2, Title = "secondCategory", Image = "secondImage" });
            dbContext.Add(new Category { Id = 3, Title = "thirdCategory", Image = "thirdImage" });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);

            await service.EditById(2, "secondCategoryNew");
            Assert.Equal("secondCategoryNew", dbContext.Categories.FirstOrDefaultAsync(x => x.Id == 2).Result.Title);
            Assert.DoesNotContain(new Category { Id = 2, Title = "secondCategory", Image = "secondImage" }, dbContext.Categories);
            await Assert.ThrowsAnyAsync<Exception>(() => service.EditById(222, "asdf"));
        }
    }
}

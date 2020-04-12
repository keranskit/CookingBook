namespace CookingBook.Services.Data.Tests
{
    using System.Linq;

    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
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
    }
}

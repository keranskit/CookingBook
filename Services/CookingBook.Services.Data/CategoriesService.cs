namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using Mapping;
    using Web.ViewModels.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public int GetCount()
        {
            return this.categoryRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.categoryRepository.All().To<T>().ToList();
        }

        public async Task AddAsync(CategoriesViewModel categoriesViewModel)
        {
            Category newCategory = new Category
            {
                Title = categoriesViewModel.Title,
            };
            await this.categoryRepository.AddAsync(newCategory);
        }
    }
}

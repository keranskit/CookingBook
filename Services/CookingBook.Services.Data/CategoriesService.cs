namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

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
            return this.categoryRepository.All().Where(x => x.IsDeleted == false).To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var category = this.categoryRepository.All()
                .Where(x => x.Id == id)
                .Where(x => x.IsDeleted == false)
                .To<T>().FirstOrDefault();
            return category;
        }
    }
}

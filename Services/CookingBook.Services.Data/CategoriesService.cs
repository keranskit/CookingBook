namespace CookingBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteById(int id)
        {
            var category = this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id).Result;
            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task EditById(int id, string newName)
        {
            var category = this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id).Result;
            category.Title = newName;
            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }
    }
}

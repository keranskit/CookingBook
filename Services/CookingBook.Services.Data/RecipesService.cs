namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;

        public RecipesService(IDeletableEntityRepository<Recipe> recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        public int GetCount()
        {
            return this.recipeRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.recipeRepository.All().To<T>().ToList();
        }


    }
}

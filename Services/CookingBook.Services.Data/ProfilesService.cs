namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ProfilesService : IProfilesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public ProfilesService(
            IDeletableEntityRepository<Recipe> recipesRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.recipesRepository = recipesRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<T> GetByUserId<T>(string userId)
        {
            return this.recipesRepository.All().Where(x => x.User.Id == userId).To<T>().ToList();
        }

        public IEnumerable<T> GetFavoriteByUserId<T>(string userId)
        {
            return this.userRepository.All().Where(x => x.Id == userId).SelectMany(x => x.FavoriteRecipes).To<T>().ToList();

            // var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId);
            // var favoriteRecipes = user.FavoriteRecipes.
        }

        public async Task RemoveFromFavorites(string userId, string recipeId)
        {
            var collection = this.userRepository.All().Where(x => x.Id == userId).SelectMany(x => x.FavoriteRecipes)
                .ToList();
            var itemToRemove = collection.Find(x => x.RecipeId == recipeId);
            if (itemToRemove != null)
            {
                collection.Remove(itemToRemove);
                this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId).Result.FavoriteRecipes = collection;
            }

            await this.userRepository.SaveChangesAsync();
        }
    }
}

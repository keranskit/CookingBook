namespace CookingBook.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using Data.Models;
    using Services.Mapping;

    public class RecipeByIdViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }
        
        public string Title { get; set; }

        public string Photo { get; set; }

        public string ProductId { get; set; }

        public virtual Product Products { get; set; }

        public string CookProcedure { get; set; }

        public int CookTime { get; set; }

        public int Serving { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string NutritionValueId { get; set; }

        public virtual NutritionValue NutritionValue { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<UserFavoriteRecipe> FavoriteBy { get; set; }

        public virtual ICollection<UserCookedRecipe> CookedBy { get; set; }

        public string ReviewId { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}

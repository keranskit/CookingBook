namespace CookingBook.Web.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeByIdViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public string Photo { get; set; }

        public ICollection<RecipeByIdProductsViewModel> Products { get; set; }

        public string CookProcedure { get; set; }

        public int CookTime { get; set; }

        public int Serving { get; set; }

        public string CategoryTitle { get; set; }

        public NutritionValueForRecipeViewModel NutritionValue { get; set; }

        public string UserName { get; set; }

        public int FavoriteBy { get; set; }

        // public virtual ICollection<UserCookedRecipe> CookedBy { get; set; }
        public ICollection<ReviewForRecipeViewModel> Reviews { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeByIdViewModel>()
                .ForMember(x => x.FavoriteBy, s => s.MapFrom(x => x.FavoriteBy.Count));
        }
    }
}

namespace CookingBook.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeCreateNutritionValuesViewModel : IMapTo<NutritionValue>, IMapFrom<NutritionValue>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Calories { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Fats { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Carbohydrates { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Sugar { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Protein { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Fiber { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Salt { get; set; }
    }
}

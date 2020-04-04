namespace CookingBook.Web.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeCreateViewModel : IMapTo<Recipe>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Photo { get; set; }

        public string ProductId { get; set; }

        public Product Products { get; set; }

        [Required]
        public string CookProcedure { get; set; }

        [Required]
        [Range(1, 60 * 24)]
        [Display(Name = "CookTimeInMinutes")]
        public int CookTime { get; set; }

        [Required]
        [Range(1, 60 * 24)]
        public int Serving { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }

        public string NutritionValueId { get; set; }

        public NutritionValue NutritionValue { get; set; }
    }
}

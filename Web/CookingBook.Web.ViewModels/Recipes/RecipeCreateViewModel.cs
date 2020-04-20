namespace CookingBook.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using Ganss.XSS;

    public class RecipeCreateViewModel : IMapTo<RecipeDTO>
    {
        public RecipeCreateViewModel()
        {
            this.Products = new HashSet<RecipeCreateProductsViewModel>();
        }

        [Required]
        public string Title { get; set; }

        public string SanitizedTitle 
            => new HtmlSanitizer().Sanitize(this.Title);

        [Required]
        public string Photo { get; set; }

        [Required]
        public ICollection<RecipeCreateProductsViewModel> Products { get; set; }

        [Required]
        public string CookProcedure { get; set; }

        public string SanitizedCookProcedure 
            => new HtmlSanitizer().Sanitize(this.CookProcedure);

        [Required]
        [Range(1, 60 * 24)]
        [Display(Name = "CookTimeInMinutes")]
        public int CookTime { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Serving { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }

        public RecipeCreateNutritionValuesViewModel NutritionValue { get; set; }
    }
}

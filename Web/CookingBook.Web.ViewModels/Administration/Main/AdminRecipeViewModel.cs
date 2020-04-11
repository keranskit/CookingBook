namespace CookingBook.Web.ViewModels.Administration.Main
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Recipes;
    using Ganss.XSS;

    public class AdminRecipeViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string SanitizedTitle
            => new HtmlSanitizer().Sanitize(this.Title);

        [Required]
        public string Photo { get; set; }

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
    }
}

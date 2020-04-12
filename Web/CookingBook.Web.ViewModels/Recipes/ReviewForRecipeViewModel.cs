namespace CookingBook.Web.ViewModels.Recipes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using Ganss.XSS;

    public class ReviewForRecipeViewModel : IMapFrom<Review>
    {
        [Required]
        public string Comment { get; set; }

        public string RecipeId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public string SanitizedReview => new HtmlSanitizer().Sanitize(this.Comment);
    }
}

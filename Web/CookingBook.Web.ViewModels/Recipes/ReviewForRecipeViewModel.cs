namespace CookingBook.Web.ViewModels.Recipes
{
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using Ganss.XSS;

    public class ReviewForRecipeViewModel : IMapFrom<Review>
    {
        public string Comment { get; set; }

        public string RecipeId { get; set; }

        public string UserId { get; set; }

        public string SanitizedReview => new HtmlSanitizer().Sanitize(this.Comment);
    }
}
